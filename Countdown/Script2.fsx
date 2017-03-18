#r @"..\packages\FSPowerPack.Parallel.Seq.Community.3.0.0.0\Lib\Net40\FSharp.PowerPack.Parallel.Seq.dll"
#r @"..\packages\MyFSharpHelpers.1.0.1.0\lib\net461\MyFSharpHelpers.dll"

//[0;1;2;3;4;5;6;7;8;9]
//|> Permutations.PGetAllPerms

//#r @"..\packages\FSPowerPack.Parallel.Seq.Community.3.0.0.0\Lib\Net40\FSharp.PowerPack.Parallel.Seq.dll"
//
//open Microsoft.FSharp.Collections
//
//[0;1;2;3;4;5;6;7;8;9]
//|> PSeq.map (id)


#load "StackCalculator2.fs"
#load "GetCombinations.fs"
#load "CountdownSolver.fs"

open StackCalculator2
open CountdownSolver
open GetCombinations
open Permutations
open Microsoft.FSharp.Collections

// 1 2 + 3 + = 6 
// 1 2 + 3 * = 9
// 1 2 * 3 + = 5
// 1 2 * 3 * = 6

// http://happysoft.org.uk/countdown/numgame.php
//let numbers = [Number(1);Number(2);Number(3);Number(4);Number(5);Number(6)]
//let numbers = [Number(75);Number(100);Number(3);Number(4);Number(5);Number(6)]
//let numbers = [Number(75);Number(100);Number(4);Number(5);Number(6)]
//let numbers = [Number(75);Number(100);Number(5);Number(6);Number(4)]
//let numbers = [75;100;5;6;4]
//let numbers = ["75";"100";"5";"6";"4"]
//let operators = ["+";"-";"*";"/"]

//numbers 
//|> getTotalsForNumberList
//|> Seq.iter (printfn "%A")

//let numbers = [75;100;5;6;4]

//let numbers, target = [100;75;6;5;4] , 744
//let numbers, target = [3; 7; 6; 2; 1; 7] , 824
// 2, 4, 5, 9, 10 and 100 into a figure of 566.
let numbers, target = [75;6;50;100;3;25], 952
//let numbers, target = [1;2;3;4;5;6;7;8;9;10], 952


let numbersToItemNumbers numbers =
    numbers 
    |> Seq.map (fun n->Item.Number(n))

let stringifyItems (items : Item seq) = 
    items 
    |> Seq.map (sprintf "%O")
    |> Seq.reduce (fun a b -> sprintf "%s %s" a b)

let printResults (total, items) = sprintf "%d -> %s" total (items |> stringifyItems)

#time "on" 

//numbers 
//|> permutations 
//|> Seq.map numbersToItemNumbers
//|> Seq.collect getTotalsForNumberList
//|> Seq.filter (fun total -> total = target)
////|> Seq.find ( (=) target)
////|> Seq.distinct
////|> Seq.length
//|> Seq.iter (printfn "%A")
////|> Seq.length |> printfn "%d"

numbers 
|> PGetAllPerms
|> PSeq.map numbersToItemNumbers
|> PSeq.collect getTotalsForNumberList
|> PSeq.filter (fun (total, _) -> total = target)
|> PSeq.map printResults
//|> Seq.find ( (=) target)
//|> Seq.distinct
//|> Seq.length
|> Seq.iter (printfn "%A")
//|> Seq.length |> printfn "%d"


// TODO
// - Integer maths -> should use double or decimal
// - Less than all the numbers
// - http://ccg.doc.gold.ac.uk/papers/colton_aisb14a.pdf



[Plus;Number(123)] |> stringifyItems

