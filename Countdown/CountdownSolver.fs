module CountdownSolver

open StackCalculator2

let mergeOperatorsIntoNumbers numbers (operators : 'T list) =
    numbers
    |> Seq.mapi(fun i x ->
        match i with 
        | 0 -> [x]
        | i -> [x; (operators.[i-1]) ] )
    |> Seq.collect id


