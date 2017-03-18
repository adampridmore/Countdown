module CountdownSolver

open StackCalculator2
open GetCombinations

let mergeOperatorsIntoNumbers numbers (operators : 'T seq) =
    let operatorsArray = operators |> Seq.toArray

    numbers
    |> Seq.mapi(fun i x ->
        match i with 
        | 0 -> [x]
        | i -> [x; (operatorsArray.[i-1]) ] )
    |> Seq.collect id


let getTotalsForNumberList (numbers: Item seq) = 

    let operators = [Plus;Minus;Multiply;Divide]
    let numberOfOpertors = (numbers |> Seq.length) - 1

    getCombinations2 numberOfOpertors operators
    |> Seq.map (fun operatorSequence -> mergeOperatorsIntoNumbers numbers operatorSequence)
    |> Seq.map (fun operatorSequence -> (operatorSequence |> execute, operatorSequence) )
