module NumbersSolver

open StackCalculator2
open ExpressionTree
open TreeGenerator

/// Extract Operator from Item
let private getOp = function
    | Operator op -> op
    | _ -> failwith "Not an operator"

let private plusOp = getOp Plus
let private minusOp = getOp Minus
let private mulOp = getOp Multiply
let private divOp = getOp Divide

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
    let n = List.length numbers
    if n = 0 then Seq.empty else
    
    let maxMask = (1 <<< n) - 1
    let memo = Array.create (maxMask + 1) []
    
    for i in 0 .. n - 1 do
        let num = numbers.[i]
        let mask = 1 <<< i
        memo.[mask] <- [ (num, Num num, []) ]
    
    for size in 2 .. n do
        for mask in 1 .. maxMask do
            let mutable bitCount = 0
            let mutable temp = mask
            while temp > 0 do
                bitCount <- bitCount + (temp &&& 1)
                temp <- temp >>> 1
                
            if bitCount = size then
                let resultsForMask = ResizeArray<int * Expr * Step list>()
                let lowestBit = mask &&& -mask
                let remainingMask = mask ^^^ lowestBit
                
                let mutable submask = remainingMask
                while submask > 0 do
                    let rightMask = submask
                    let leftMask = mask ^^^ rightMask
                    
                    for lVal, lExpr, lSteps in memo.[leftMask] do
                        for rVal, rExpr, rSteps in memo.[rightMask] do
                            
                            let addVal = lVal + rVal
                            let addSteps = lSteps @ rSteps @ [{ Left = lVal; Op = "+"; Right = rVal; Result = addVal }]
                            resultsForMask.Add(addVal, BinOp(lExpr, plusOp, rExpr), addSteps)
                            
                            let mulVal = lVal * rVal
                            let mulSteps = lSteps @ rSteps @ [{ Left = lVal; Op = "*"; Right = rVal; Result = mulVal }]
                            resultsForMask.Add(mulVal, BinOp(lExpr, mulOp, rExpr), mulSteps)
                            
                            let subVal1 = lVal - rVal
                            if subVal1 >= 0 then
                                let subSteps = lSteps @ rSteps @ [{ Left = lVal; Op = "-"; Right = rVal; Result = subVal1 }]
                                resultsForMask.Add(subVal1, BinOp(lExpr, minusOp, rExpr), subSteps)
                                
                            let subVal2 = rVal - lVal
                            if subVal2 >= 0 then
                                let subSteps = rSteps @ lSteps @ [{ Left = rVal; Op = "-"; Right = lVal; Result = subVal2 }]
                                resultsForMask.Add(subVal2, BinOp(rExpr, minusOp, lExpr), subSteps)

                            if rVal <> 0 && lVal % rVal = 0 then
                                let divVal1 = lVal / rVal
                                let divSteps = lSteps @ rSteps @ [{ Left = lVal; Op = "/"; Right = rVal; Result = divVal1 }]
                                resultsForMask.Add(divVal1, BinOp(lExpr, divOp, rExpr), divSteps)
                                
                            if lVal <> 0 && rVal % lVal = 0 then
                                let divVal2 = rVal / lVal
                                let divSteps = rSteps @ lSteps @ [{ Left = rVal; Op = "/"; Right = lVal; Result = divVal2 }]
                                resultsForMask.Add(divVal2, BinOp(rExpr, divOp, lExpr), divSteps)

                    submask <- (submask - 1) &&& remainingMask
                
                memo.[mask] <- resultsForMask |> Seq.toList
    
    seq {
        for mask in 1 .. maxMask do
            for rVal, expr, steps in memo.[mask] do
                if rVal = target then
                    yield { Expression = expr; Result = rVal; Steps = steps }
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
