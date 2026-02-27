module Countdown

open StackCalculator2
open ExpressionTree
open NumbersSolver

let doNumbers (numbers:string) (target: string) =
  let targetNumber = target |> System.Int32.Parse
  let numberList = numbers |> parseStringToStack

  solveBest numberList targetNumber 5
  |> List.iter (fun solution ->
      printfn "%s = %d" (toInfix solution.Expression) solution.Result)

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
