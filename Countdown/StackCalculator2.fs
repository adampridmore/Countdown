module StackCalculator2

[<StructuredFormatDisplay("{name}")>]
type Operator = {
    fn : (int -> int -> int)
    name : string
}

type Item = 
    | Operator of Operator
    | Number of int
    override m.ToString() = 
        match m with
        | Operator o -> o.name
        | Number n -> n.ToString()

let Plus        = Operator({fn = (+);     name = "+"})
let Minus       = Operator({fn = (-);     name = "-"})
let Multiply    = Operator({fn = (*);     name = "*"})
let Divide      = Operator({fn = (/);     name = "/"})


//type Items = Items of List<Item>

let executeTop = 
    function
    | Number(a)::Number(b)::Operator(o)::rest -> Number(o.fn a b)::rest
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
