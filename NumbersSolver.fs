module NumbersSolver

open ExpressionTree
open TreeGenerator

/// A solution to a numbers puzzle
type Solution = {
    Expression: Expr
    Result: decimal
    Steps: Step list
}

/// Score a solution - lower is better
/// Factors: fewer steps, avoid division, prefer multiplication over subtraction
let scoreSolution (solution: Solution) : int =
    let steps = solution.Steps
    let stepCount = steps.Length

    // Count operations by type
    let divCount = steps |> List.filter (fun s -> s.Op = "/") |> List.length
    let subCount = steps |> List.filter (fun s -> s.Op = "-") |> List.length

    // Check for "nice" intermediate results (multiples of 10, 25, etc.)
    let niceResults =
        steps
        |> List.filter (fun s ->
            s.Result % 100m = 0m ||
            s.Result % 25m = 0m ||
            s.Result % 10m = 0m)
        |> List.length

    // Scoring: lower is better
    // - Each step costs 100 points (most important factor)
    // - Division costs 30 points per use (harder to do mentally)
    // - Subtraction costs 10 points per use (slightly harder than addition)
    // - Nice intermediate results give -15 bonus each
    (stepCount * 100) + (divCount * 30) + (subCount * 10) - (niceResults * 15)

/// Find all solutions that reach the target number
let solve (numbers: decimal list) (target: decimal) : Solution seq =
    seq {
        for expr in allExpressionsFor numbers do
            let (result, steps) = evaluateWithSteps expr
            match result with
            | Some r when r = target ->
                yield { Expression = expr; Result = r; Steps = steps }
            | _ -> ()
    }

/// Find all solutions, deduplicated by normalized infix representation
let solveUnique (numbers: decimal list) (target: decimal) : Solution seq =
    solve numbers target
    |> Seq.map (fun sol -> { sol with Expression = normalize sol.Expression })
    |> Seq.distinctBy (fun sol -> toInfix sol.Expression)

/// Find the first solution (for performance when only one is needed)
let solveFirst (numbers: decimal list) (target: decimal) : Solution option =
    solve numbers target |> Seq.tryHead

/// Find the best solutions, ranked by score (lower is better), returning top N
let solveBest (numbers: decimal list) (target: decimal) (count: int) : Solution list =
    solveUnique numbers target
    |> Seq.toList
    |> List.sortBy scoreSolution
    |> List.truncate count
