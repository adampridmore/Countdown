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
    let rec executeRest items = 
        match items with
        | [Number(x)] -> [Number(x)]
        | x -> items |> executeTop |> executeRest

    match (items |> executeRest) with
    | [Number(x)] -> x
    | x -> failwith "Error"
