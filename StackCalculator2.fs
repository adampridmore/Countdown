module StackCalculator2

type num = int

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

let Plus     = Operator {fn = (+);                                               name = "+"}
let Minus    = Operator {fn = (-);                                               name = "-"}
let Multiply = Operator {fn = (*);                                               name = "*"}
let Divide   = Operator {fn = (fun a b -> if b <> 0 && a % b = 0 then a/b else -1); name = "/"}

let itemToNum item =
    match item with
    | Number x -> x
    | _ -> failwith "Operator is not a number"

let isValid (n: num) = n >= 0

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

let execute2 (items : seq<Item>) =
    let itemsList = items |> Seq.toList

    let rec executeRest (results,itemsList) =
        match itemsList with
        | [Number(x)] -> (results, [])
        | itemsList ->
            let newStack = itemsList |> executeTop
            let topNumber = newStack |> Seq.head |> itemToNum
            match topNumber |> isValid with
            | true -> executeRest (topNumber::results, newStack)
            | false -> (results, [itemsList |> Seq.head ] )

    let results, _ = executeRest ([], itemsList)
    results

let parseStringToStack : (string -> list<num>) =
    let split (text:string) =
        text.Split([|" "|],System.StringSplitOptions.RemoveEmptyEntries)

    let parse (item:string) =
        match System.Int32.TryParse item with
        | true, i -> i
        | false, _ -> failwith (sprintf "Invalid number: %s" item)

    split >> Seq.map parse >> Seq.toList

let numbersToItemNumbers (numbers: seq<num>) : seq<Item> =
    numbers
    |> Seq.map (fun n->Item.Number(n))
