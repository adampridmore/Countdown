module NumbersSolverTests

open StackCalculator2
open ExpressionTree
open TreeGenerator
open NumbersSolver
open FsUnit
open Xunit

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
    let solutions = solve [2m; 3m; 4m] 14m |> Seq.toList
    solutions |> List.isEmpty |> should equal false
    solutions |> List.exists (fun s -> s.Result = 14m) |> should equal true

// Test infix formatting produces correct brackets
[<Fact>]
let ``Infix format for 2 * (3 + 4)``() =
    let expr = BinOp(Num 2m, multiply, BinOp(Num 3m, plus, Num 4m))
    toInfix expr |> should equal "2 * (3 + 4)"

[<Fact>]
let ``Infix format for (2 + 3) * 4``() =
    let expr = BinOp(BinOp(Num 2m, plus, Num 3m), multiply, Num 4m)
    toInfix expr |> should equal "(2 + 3) * 4"

// Test that unnecessary brackets are omitted
[<Fact>]
let ``Infix format omits unnecessary brackets for same precedence left``() =
    // 2 + 3 + 4 should not have brackets
    let expr = BinOp(BinOp(Num 2m, plus, Num 3m), plus, Num 4m)
    toInfix expr |> should equal "2 + 3 + 4"

[<Fact>]
let ``Infix format adds brackets for subtraction on right``() =
    // 2 - (3 - 4) needs brackets because subtraction is not associative
    let expr = BinOp(Num 2m, minus, BinOp(Num 3m, minus, Num 4m))
    toInfix expr |> should equal "2 - (3 - 4)"

// Test evaluation with game rules
[<Fact>]
let ``Evaluate rejects negative intermediate results``() =
    // 2 - 5 = -3 (invalid)
    let expr = BinOp(Num 2m, minus, Num 5m)
    evaluate expr |> should equal None

[<Fact>]
let ``Evaluate rejects fractional results``() =
    // 3 / 2 = 1.5 (invalid)
    let expr = BinOp(Num 3m, divide, Num 2m)
    evaluate expr |> should equal None

[<Fact>]
let ``Evaluate accepts valid division``() =
    // 6 / 2 = 3 (valid)
    let expr = BinOp(Num 6m, divide, Num 2m)
    evaluate expr |> should equal (Some 3m)

[<Fact>]
let ``Evaluate rejects division by zero``() =
    let expr = BinOp(Num 6m, divide, Num 0m)
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
    let allSolutions = solve [2m; 3m; 4m] 14m |> Seq.length
    let uniqueSolutions = solveUnique [2m; 3m; 4m] 14m |> Seq.length
    uniqueSolutions |> should be (lessThanOrEqualTo allSolutions)

// Test a simple known puzzle
[<Fact>]
let ``Can solve simple addition``() =
    let solutions = solve [1m; 2m; 3m] 6m |> Seq.toList
    solutions |> List.isEmpty |> should equal false

// Test using subset of numbers
[<Fact>]
let ``Can find solution using subset of numbers``() =
    // Target 10 from [2, 5, 7] - should find 2 * 5 = 10 using only 2 numbers
    let solutions = solve [2m; 5m; 7m] 10m |> Seq.toList
    solutions |> List.isEmpty |> should equal false
