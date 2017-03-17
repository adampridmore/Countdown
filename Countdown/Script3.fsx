#load "GetCombinations.fs"
open GetCombinations

getCombinations 2 10
|> Seq.map (Seq.toList)
|> Seq.map (List.rev)
|> Seq.iter (printfn "%A")
