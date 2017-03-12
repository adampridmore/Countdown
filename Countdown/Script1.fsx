let n = [1;2;3]

let apply (a,b) =
    [(a + b);
    (a - b);
    (a / b);
    (a * b)]

apply (1,2)


type Operator = Plus | Minus | Multiply | Divide
type Item = 
    | Operator of Operator
    | Number of int

type Items = 
    | Item of List<Item>

let evaluate operator a b =
    match operator with
    | Plus -> a + b
    | Minus -> a - b
    | Multiply -> a * b
    | Divide -> a / b

let rec evaluateItems items = 
    match items with
    | Operator o :: Number(a) :: Number(b) :: [] -> evaluate o a b
    | Operator o :: Number(a) :: rest -> evaluate o a (evaluateItems rest)
    | [] -> 0
    | Number(a) :: [] -> a
    | Number(_) :: _ -> failwith "Items cannot start with number and more items"
    | Operator(_) :: Operator(_) :: _ -> failwith "Items cannot start with two operarors"
    | Operator(_) :: _ -> failwith "Items cannot start with just one operarors"
   
[Operator(Plus);Number(1); Number(2)] |> evaluateItems 

// + 1 * 2 3 
// 1 + (2*3) = 7
[Operator(Plus);Number(1); Operator(Multiply); Number(2);Number(3)] |> evaluateItems 

[Number(1)] |> evaluateItems

[Operator(Plus)] |> evaluateItems

