//let numbers = [0;1;2]
//
//let unfolder state = 
//    let mapfolder state item = 
//        (state, item )
//    
//    state 
//    |> Seq.mapFold mapfolder false
//
//    //Some(2,state)
//
//Seq.unfold unfolder [0;0;0]
//|> Seq.take 10

let next max numbers = 
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
        | (nextState, _) -> Some(state, nextState)
    
    Seq.unfold unfolder numbers

getCombinations 5 10
|> Seq.map (Seq.toList)
|> Seq.last
|> Seq.singleton
|> Seq.iter (printfn "%A")


//let unfolder (state:int seq) = 
//    Some(state, next max state)
//    
//Seq.unfold unfolder (numbers |> List.toSeq)
//|> Seq.take 20
//|> Seq.toList
