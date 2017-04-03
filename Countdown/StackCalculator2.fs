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

let itemToDecimal num =
    match num with
    | Number(x) -> x |> decimal
    | _ -> failwith "Operator is not a number"

let isValid num =  
    match num with
    | x when x < 0m -> false
    | x when x - System.Math.Floor(x) > 0m -> false
    | _ -> true

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

let execute2 items = 
    let itemsList = items |> Seq.toList

    let rec executeRest (results,itemsList) = 
        match itemsList with
        | [Number(x)] -> (results, [])
        | itemsList -> 
            let newStack = itemsList |> executeTop 
            let topNumber = newStack |> Seq.head |> itemToDecimal
            match topNumber |> isValid with
            | true -> executeRest (topNumber::results, newStack)
            | false -> (results, [itemsList |> Seq.head ] )

    let results, _ = executeRest ([], itemsList) 
    results

let parseStringToStack = 
    let split (text:string) =
        text.Split([|" "|],System.StringSplitOptions.RemoveEmptyEntries)
   
    let tryPaseInt text =
        let mutable i = 0
        match (System.Int32.TryParse(text, &i)) with
        | true -> Some(i)
        | false -> None

    let parse (item:string) =
        match tryPaseInt(item) with
        | Some(x) -> x |> decimal
        | None -> failwith (sprintf "Invalid number: %s" item)

    split >> Seq.map parse >> Seq.toList

let numbersToItemNumbers numbers =
    numbers 
    |> Seq.map (fun n->Item.Number(n))
