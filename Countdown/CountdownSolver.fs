module CountdownSolver

open StackCalculator2

let mergeOperatorsIntoNumbers numbers (operators : 'T seq) =
    let operatorsArray = operators |> Seq.toArray

    numbers
    |> Seq.mapi(fun i x ->
        match i with 
        | 0 -> [x]
        | i -> [x; (operatorsArray.[i-1]) ] )
    |> Seq.collect id


