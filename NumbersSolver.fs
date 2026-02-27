module NumbersSolver

open ExpressionTree
open TreeGenerator

/// A solution to a numbers puzzle
type Solution = {
    Expression: Expr
    Result: int
    Steps: Step list
}

/// Score a solution - lower is better
/// Factors: fewer steps, avoid division, prefer multiplication over subtraction
let scoreSolution (solution: Solution) : int =
    let steps = solution.Steps
    let stepCount = steps.Length

    // Count operations and nice results in a single pass
    let mutable divCount = 0
    let mutable subCount = 0
    let mutable niceResults = 0
    for s in steps do
        if s.Op = "/" then divCount <- divCount + 1
        if s.Op = "-" then subCount <- subCount + 1
        if s.Result % 100 = 0 || s.Result % 25 = 0 || s.Result % 10 = 0 then
            niceResults <- niceResults + 1

    // Scoring: lower is better
    // - Each step costs 100 points (most important factor)
    // - Division costs 30 points per use (harder to do mentally)
    // - Subtraction costs 10 points per use (slightly harder than addition)
    // - Nice intermediate results give -15 bonus each
    stepCount * 100 + divCount * 30 + subCount * 10 - niceResults * 15

/// Find all solutions that reach the target number
let solve (numbers: int list) (target: int) : Solution seq =
    seq {
        for expr in allExpressionsFor numbers do
            let result, steps = evaluateWithSteps expr
            match result with
            | Some r when r = target ->
                yield { Expression = expr; Result = r; Steps = steps }
            | _ -> ()
    }

/// Find all solutions, deduplicated by normalized infix representation
let solveUnique (numbers: int list) (target: int) : Solution seq =
    solve numbers target
    |> Seq.map (fun sol -> { sol with Expression = normalize sol.Expression })
    |> Seq.distinctBy (fun sol -> toInfix sol.Expression)

/// Find the first solution (for performance when only one is needed)
let solveFirst (numbers: int list) (target: int) : Solution option =
    solve numbers target |> Seq.tryHead

/// Find the best solutions, ranked by score (lower is better), returning top N
let solveBest (numbers: int list) (target: int) (count: int) : Solution list =
    solveUnique numbers target
    |> Seq.toList
    |> List.sortBy scoreSolution
    |> List.truncate count
