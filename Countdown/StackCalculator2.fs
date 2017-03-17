module StackCalculator2

type Item = 
    | Operator of (int -> int -> int)
    | Number of int

//type Items = Items of List<Item>

let executeTop = 
    function
    | Number(a)::Number(b)::Operator(o)::rest -> Number(o a b)::rest
    | [Number(x)] -> [Number(x)]
    | _ -> failwith "Error"

let execute items = 
    let itemsList = items |> Seq.toList

    let rec executeRest itemsList = 
        match itemsList with
        | [Number(x)] -> [Number(x)]
        | x -> itemsList |> executeTop |> executeRest

    match (itemsList |> executeRest) with
    | [Number(x)] -> x
    | x -> failwith "Error"
