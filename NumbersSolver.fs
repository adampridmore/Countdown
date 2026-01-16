module NumbersSolver

open ExpressionTree
open TreeGenerator

/// A solution to a numbers puzzle
type Solution = {
    Expression: Expr
    Result: decimal
    Steps: Step list
}

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

/// Find all solutions, deduplicated by their infix string representation
let solveUnique (numbers: decimal list) (target: decimal) : Solution seq =
    solve numbers target
    |> Seq.distinctBy (fun sol -> toInfix sol.Expression)

/// Find the first solution (for performance when only one is needed)
let solveFirst (numbers: decimal list) (target: decimal) : Solution option =
    solve numbers target |> Seq.tryHead
