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



let getTotalsForNumberList (numbers: Item list) = 

    let operators = [Operator(+);Operator(-);Operator(*);Operator(/)]

    let numberOfOpertors = numbers.Length - 1

    getCombinations2 numberOfOpertors operators
    |> Seq.map (fun operatorSequence -> mergeOperatorsIntoNumbers numbers operatorSequence)
    |> Seq.map execute