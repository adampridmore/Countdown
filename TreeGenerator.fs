module TreeGenerator

open StackCalculator2
open ExpressionTree
open Permutations

/// The four basic operators used in Countdown (extract Operator records from Item wrappers)
let private operators =
    let extractOp = function
        | Operator op -> op
        | _ -> failwith "Not an operator"
    [Plus; Minus; Multiply; Divide] |> List.map extractOp

/// Generate all ways to split a list into two non-empty parts
let allSplits (items: 'a list) : ('a list * 'a list) seq =
    seq {
        for i in 1 .. (List.length items - 1) do
            yield (List.take i items, List.skip i items)
    }

/// Generate all non-empty subsets of a list
let allSubsets (items: 'a list) : 'a list seq =
    let rec subsets = function
        | [] -> seq { yield [] }
        | x :: xs ->
            seq {
                for subset in subsets xs do
                    yield subset
                    yield x :: subset
            }
    subsets items

/// Generate all binary expression trees for a list of numbers
/// Each tree represents a different bracketing/evaluation order
let rec allTrees (numbers: decimal list) : Expr seq =
    seq {
        match numbers with
        | [] -> ()
        | [n] -> yield Num n
        | nums ->
            for (leftNums, rightNums) in allSplits nums do
                for leftTree in allTrees leftNums do
                    for rightTree in allTrees rightNums do
                        for op in operators do
                            yield BinOp(leftTree, op, rightTree)
    }

/// Generate all possible expression trees for all subsets and permutations of given numbers
let allExpressionsFor (numbers: decimal list) : Expr seq =
    seq {
        // All subsets of size >= 2 (need at least 2 numbers for an operation)
        for subset in allSubsets numbers do
            if List.length subset >= 2 then
                // All permutations of each subset
                for perm in getAllPerms subset do
                    // All possible expression trees for this permutation
                    yield! allTrees perm
    }
