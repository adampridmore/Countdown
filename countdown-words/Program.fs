open WordSolver

let [<EntryPoint>] main args = 

  let doLetters letters =
    solve(letters)
    |> Seq.iter(fun (word, length) -> (printfn "Answer: %s (%d)" word length))

  let printhelp = 
    printfn "usage dotnet [-l|-n] [letters|numbers]"

  match (args |> Array.toList) with
  | "-l" :: letters :: [] -> letters |> doLetters
  // | "-n" :: letters :: [] -> letters |> doLetters
  | _ -> printhelp

  0
