﻿let next max numbers = 
    let mapfolder (carry: int) (item:int) = 
        match (carry, item) with
        | 1, item when item < (max - 1) -> (item+1, 0)
        | 1, item when item >= (max - 1) -> (0, 1)
        | 0, item -> (item, 0)
        | x -> failwith (sprintf "Overflow: %A" x)

    numbers |> Seq.mapFold mapfolder 1

let getCombinations length max = 
    let numbers = Array.create length 0 |> Array.toSeq
    let last = Array.create length max |> Array.toSeq

    let unfolder (state:int seq) = 
        let nextStateWithCarry = state |> next max
        match nextStateWithCarry with
        | (_,1) -> None
        | (nextState, _) -> Some(nextState, nextState)
    
    seq{
        yield numbers
        yield! Seq.unfold unfolder numbers
    }

getCombinations 2 10
|> Seq.map (Seq.toList)
|> Seq.map (List.rev)
|> Seq.iter (printfn "%A")
