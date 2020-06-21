module Program

open StackCalculator2
open CountdownSolver
open GetCombinations
open Permutations
open Microsoft.FSharp.Collections

let [<EntryPoint>] main _ =   
  
  let numbers, target = "4 25 1 1 1 1", 100m // Easy

  let stringifyItems (items : Item seq) = 
      items 
      |> Seq.map (sprintf "%O")
      |> Seq.reduce (fun a b -> sprintf "%s %s" a b)

  let printResults ( (total: num) , items) = sprintf "%0.0f -> %s" total (items |> stringifyItems)
  
  let printResults2 ( (totals: List<num>) , items) = 
      let totalsText = totals |> Seq.map (sprintf "%0.0f" )|> Seq.reduce (sprintf "%s,%s")
      sprintf "%s -> %s" totalsText (items |> stringifyItems)

  numbers 
  |> parseStringToStack
  |> getAllPerms
  |> Seq.map numbersToItemNumbers
  |> Seq.collect getTotalsForNumberList
  |> Seq.filter (fun (totals, _) -> totals |> Seq.exists(fun total -> total = target) )
  |> Seq.map printResults2
  |> Seq.iter (printfn "%A")

  
  0
