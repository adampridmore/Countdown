#load "StackCalculator2.fs"

open StackCalculator2

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

"1 2 3 + + +" |> parseStringToStack


