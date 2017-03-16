#load "StackCalculator2.fs"
open StackCalculator2

// 1 2 3 4 5
// 1 2 + 4 + 5 +
//[Number(1);Number(2);Operator(+);Number(4);Operator(+);Number(5);Operator(+)]
//|> execute

let mergeOperatorsIntoNumbers numbers (operators : 'T list) =
    numbers
    |> Seq.mapi(fun i x ->
        match i with 
        | 0 -> [x]
        | i -> [x; (operators.[i-1]) ] )
    |> Seq.collect id

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

let operatorCombinationsSeq =
    let operators = [Operator(+);Operator(*);Operator(-);Operator(/)]

    let unfolder state = 
        //state |> Seq.mapFold 

        Some(2,state)

    Seq.unfold unfolder [Operator(+);Operator(+);Operator(+);Operator(+)]

//
//
//    seq {
//        for ``1`` in operators do 
//            for ``2`` in operators do 
//                for ``3`` in operators do 
//                    for ``4`` in operators do 
//                        yield [``1``;``2``;``3``;``4``]
////                        for ``5`` in operators do 
////                            yield [``1``;``2``;``3``;``4``;``5``]
//    }


// http://happysoft.org.uk/countdown/numgame.php
//let numbers = [Number(1);Number(2);Number(3);Number(4);Number(5);Number(6)]
//let numbers = [Number(75);Number(100);Number(3);Number(4);Number(5);Number(6)]
//let numbers = [Number(75);Number(100);Number(4);Number(5);Number(6)]
let numbers = [Number(75);Number(100);Number(5);Number(6);Number(4)]

let doCalculation numbers operators = 
    mergeOperatorsIntoNumbers numbers operators
    |> Seq.toList
    |> execute

operatorCombinationsSeq
|> Seq.map (doCalculation numbers)
|> Seq.iter (printfn "%d")
