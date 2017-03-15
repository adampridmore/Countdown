type Item = 
    | Operator of (int -> int -> int)
    | Number of int

type Items = Item of List<Item>

let calc = [Number(1);Number(2);Operator(+)]

let executeTop items =
    match items with
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
    | _ -> failwith "Error"

calc |> executeTop

[Number(1);Number(2);Operator(+);Number(3);Operator(*)]
|> execute
