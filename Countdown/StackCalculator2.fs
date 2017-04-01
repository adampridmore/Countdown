module StackCalculator2

type num = decimal

[<StructuredFormatDisplay("{name}")>]
type Operator = {
    fn : (num -> num -> num)
    name : string
}

type Item = 
    | Operator of Operator
    | Number of num
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

let parseStringToStack = 
    let split (text:string) =
        text.Split([|" "|],System.StringSplitOptions.RemoveEmptyEntries)
   
    let tryPaseInt text =
        let mutable i = 0
        match (System.Int32.TryParse(text, &i)) with
        | true -> Some(i)
        | false -> None

    let parseOperator text = 
        match text with
        | "+" -> Plus
        | "-" -> Minus
        | "*" -> Multiply
        | "/" -> Divide
        | x -> failwith (sprintf "Invalid item: %s" text)

    let parse (item:string) =
        match tryPaseInt(item) with
        | Some(x) -> Number(x |> decimal)
        | None -> (item |> parseOperator)


    split >> Seq.map parse
