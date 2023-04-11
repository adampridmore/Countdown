module Countdown

open CountdownSolver
open StackCalculator2
open CountdownSolver
open Permutations
open Microsoft.FSharp.Collections

let doNumbers (numbers:string) (target: string) =   
  
  let targetNumber = target |> System.Decimal.Parse

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
  |> Seq.filter (fun (totals, _) -> totals |> Seq.exists(fun total -> total = targetNumber) )
  |> Seq.map printResults2
  |> Seq.iter (printfn "%A")

let doLetters letters =
  WordSolver.solve(letters)
  |> Seq.iter(fun (word, length) -> (printfn "Answer: %s (%d)" word length))

let [<EntryPoint>] main args = 

  let printhelp() = 
    printfn """usage dotnet run [-l|-n] [letters|<"numbers" target>]
For example:
  Solve letters
    dotnet run -l abc

  Solve numbers
    dotnet run -n "1 2 3" 4    
"""

  match (args |> Array.toList) with
  | "-l" :: letters :: [] -> letters |> doLetters
  | "-n" :: numbers :: target :: [] -> doNumbers numbers target
  | "-n" :: _ -> printfn "1"
  | _ -> printhelp()

  0
