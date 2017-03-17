#load "StackCalculator2.fs"
#load "CountdownSolver.fs"
#load "GetCombinations.fs"

open StackCalculator2
open CountdownSolver
open GetCombinations

// 1 2 3 4 5
// 1 2 + 4 + 5 +
//[Number(1);Number(2);Operator(+);Number(4);Operator(+);Number(5);Operator(+)]
//|> execute

//let numbers2 = [Number(1);Number(2);Number(3)]
//let operators2 = [Operator(+); Operator(+)]
//
//operators2
//|> mergeOperatorsIntoNumbers numbers2
//|> Seq.toList
//|> execute



//operatorCombinationsSeq
//|> Seq.length 
//|> Seq.iter (printfn "%A")

// Number of operators is n -1 (for n numbers)

// 1 2 + 3 + = 6 
// 1 2 + 3 * = 9
// 1 2 * 3 + = 5
// 1 2 * 3 * = 6


// http://happysoft.org.uk/countdown/numgame.php
//let numbers = [Number(1);Number(2);Number(3);Number(4);Number(5);Number(6)]
//let numbers = [Number(75);Number(100);Number(3);Number(4);Number(5);Number(6)]
//let numbers = [Number(75);Number(100);Number(4);Number(5);Number(6)]
let numbers = [Number(75);Number(100);Number(5);Number(6);Number(4)]

let doCalculation numbers operators = 
    mergeOperatorsIntoNumbers numbers operators
    |> Seq.toList
    |> execute

// Make get combinations with with an arbitary list of things
// E.g. make generic.
//getCombinations numbers
//|> Seq.map (doCalculation numbers)
//|> Seq.iter (printfn "%d")

getCombinations 3 16
|> Seq.map (Seq.toList >> List.rev)
|> Seq.iter (printfn "%A")

let getCombinations2 length items =
    let itemsArray = items |> Seq.toArray
    let mapCombination numbers = 
        numbers |> Seq.map(fun n -> itemsArray.[n])
        
    getCombinations length (items |> Seq.length)
    |> Seq.map mapCombination

getCombinations2 3 ['a';'b']
|> Seq.iter (printfn "%A")
