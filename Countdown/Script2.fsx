type Item = 
    | Operator of (int -> int -> int)
    | Number of int

//type Items = Items of List<Item>

let executeTop items  =
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
    | x -> failwith "Error"

// 1 2 + 3 + = 6
[Number(1);Number(2);Operator(+);Number(3);Operator(+)]
|> execute

// 1 2 + 3 * = 9
[Number(1);Number(2);Operator(+);Number(3);Operator(*)]
|> execute

// 1 2 * 3 + = 5
[Number(1);Number(2);Operator(*);Number(3);Operator(+)]
|> execute

// 1 2 * 3 * = 6 
[Number(1);Number(2);Operator(*);Number(3);Operator(*)]
|> execute

// 1 2 3 4 5
// 1 2 + 4 + 5 +
[Number(1);Number(2);Operator(+);Number(4);Operator(+);Number(5);Operator(+)]
|> execute
