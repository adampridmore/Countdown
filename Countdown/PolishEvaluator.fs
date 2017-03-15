module PolishEvaluator

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

// × (− 5 6) 7
let rec evaluateItems items = 
    match items with
    | Operator o :: Number(a) :: Number(b) :: [] -> evaluate o a b
    | Operator o :: Number(a) :: rest -> evaluate o a (evaluateItems rest)
    // | Operator o :: rest -> evaluate o (evaluateItems rest)
    | [] -> 0
    | Number(a) :: [] -> a
    | Number(_) :: _ -> failwith "Items cannot start with number and more items"
    | Operator(_) :: Operator(_) :: _ -> failwith "Items cannot start with two operarors"
    | Operator(_) :: _ -> failwith "Items cannot start with just one operator"


