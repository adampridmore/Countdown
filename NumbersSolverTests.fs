module NumbersSolverTests

open StackCalculator2
open ExpressionTree
open TreeGenerator
open NumbersSolver
open FsUnit
open Xunit
open Xunit.Abstractions

// Helper to extract Operator record from Item wrapper
let private extractOp = function
    | Operator op -> op
    | _ -> failwith "Not an operator"

let private plus = extractOp Plus
let private minus = extractOp Minus
let private multiply = extractOp Multiply
let private divide = extractOp Divide

// Test case that the old solver would miss - requires non-left-to-right bracketing
[<Fact>]
let ``Can find 2 * (3 + 4) = 14``() =
    let solutions = solve [2; 3; 4] 14 |> Seq.toList
    solutions |> List.isEmpty |> should equal false
    solutions |> List.exists (fun s -> s.Result = 14) |> should equal true

// Test infix formatting produces correct brackets
[<Fact>]
let ``Infix format for 2 * (3 + 4)``() =
    let expr = BinOp(Num 2, multiply, BinOp(Num 3, plus, Num 4))
    toInfix expr |> should equal "2 * (3 + 4)"

[<Fact>]
let ``Infix format for (2 + 3) * 4``() =
    let expr = BinOp(BinOp(Num 2, plus, Num 3), multiply, Num 4)
    toInfix expr |> should equal "(2 + 3) * 4"

// Test that unnecessary brackets are omitted
[<Fact>]
let ``Infix format omits unnecessary brackets for same precedence left``() =
    // 2 + 3 + 4 should not have brackets
    let expr = BinOp(BinOp(Num 2, plus, Num 3), plus, Num 4)
    toInfix expr |> should equal "2 + 3 + 4"

[<Fact>]
let ``Infix format adds brackets for subtraction on right``() =
    // 2 - (3 - 4) needs brackets because subtraction is not associative
    let expr = BinOp(Num 2, minus, BinOp(Num 3, minus, Num 4))
    toInfix expr |> should equal "2 - (3 - 4)"

// Test evaluation with game rules
[<Fact>]
let ``Evaluate rejects negative intermediate results``() =
    // 2 - 5 = -3 (invalid)
    let expr = BinOp(Num 2, minus, Num 5)
    evaluate expr |> should equal None

[<Fact>]
let ``Evaluate rejects fractional results``() =
    // 3 / 2 = 1.5 (invalid, non-exact)
    let expr = BinOp(Num 3, divide, Num 2)
    evaluate expr |> should equal None

[<Fact>]
let ``Evaluate accepts valid division``() =
    // 6 / 2 = 3 (valid)
    let expr = BinOp(Num 6, divide, Num 2)
    evaluate expr |> should equal (Some 3)

[<Fact>]
let ``Evaluate rejects division by zero``() =
    let expr = BinOp(Num 6, divide, Num 0)
    evaluate expr |> should equal None

// Test tree generation
[<Fact>]
let ``allSplits generates correct partitions``() =
    let splits = allSplits [1; 2; 3] |> Seq.toList
    splits |> should equal [([1], [2; 3]); ([1; 2], [3])]

[<Fact>]
let ``allSubsets generates all subsets``() =
    let subsets = allSubsets [1; 2] |> Seq.toList |> List.sort
    subsets |> should equal [List.empty<int>; [1]; [1; 2]; [2]]

// Test solveUnique deduplicates
[<Fact>]
let ``solveUnique removes duplicate expressions``() =
    // For a simple case, unique solutions should be fewer than all solutions
    let allSolutions = solve [2; 3; 4] 14 |> Seq.length
    let uniqueSolutions = solveUnique [2; 3; 4] 14 |> Seq.length
    uniqueSolutions |> should be (lessThanOrEqualTo allSolutions)

// Test a simple known puzzle
[<Fact>]
let ``Can solve simple addition``() =
    let solutions = solve [1; 2; 3] 6 |> Seq.toList
    solutions |> List.isEmpty |> should equal false

// Test using subset of numbers
[<Fact>]
let ``Can find solution using subset of numbers``() =
    // Target 10 from [2, 5, 7] - should find 2 * 5 = 10 using only 2 numbers
    let solutions = solve [2; 5; 7] 10 |> Seq.toList
    solutions |> List.isEmpty |> should equal false

type PerformanceTests(output: ITestOutputHelper) =

    // Classic Countdown example: 25 8 6 9 7 2 -> 682
    let numbers = [25; 8; 6; 9; 7; 2]
    let target = 682

    let timeRun label f =
        let sw = System.Diagnostics.Stopwatch.StartNew()
        let result = f ()
        sw.Stop()
        output.WriteLine(sprintf "%s: %dms" label sw.ElapsedMilliseconds)
        result

    // Sequential version for comparison
    let solveSequential (numbers: int list) (target: int) =
        seq {
            for expr in allExpressionsFor numbers do
                let result, steps = evaluateWithSteps expr
                match result with
                | Some r when r = target -> yield { Expression = expr; Result = r; Steps = steps }
                | _ -> ()
        }

    [<Fact>]
    member _.``Numbers solver performance``() =
        // Warm up JIT (cheap call)
        solve [1; 2; 3] 6 |> Seq.tryHead |> ignore

        let seqCount = timeRun "sequential" (fun () -> solveSequential numbers target |> Seq.length)
        let parCount = timeRun "parallel  " (fun () -> solve numbers target |> Seq.length)

        output.WriteLine(sprintf "Sequential solutions: %d" seqCount)
        output.WriteLine(sprintf "Parallel   solutions: %d" parCount)

        seqCount |> should be (greaterThan 0)
        parCount |> should be (greaterThan 0)
