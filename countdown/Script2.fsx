#r @"..\packages\FSPowerPack.Parallel.Seq.Community.3.0.0.0\Lib\Net40\FSharp.PowerPack.Parallel.Seq.dll"
#r @"..\packages\MyFSharpHelpers.1.0.2.0\lib\net461\MyFSharpHelpers.dll"

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

//let numbers, target = "25 50 75 100 3 6", 952m // The famous one

// let numbers, target = "50 3 25 3 75 100", 996m // [50m;3m;25m;3m;75m;100m], 996m // Unique
let numbers, target = "4 25 1 1 1 1", 100m // Easy

//https://www.youtube.com/watch?v=n8-mx3RSvOQ
//let numbers, target = "25 100 75 50 6 4", 821m
//let numbers, target = "25 100 75 50 6 2 3", 1000m
//let numbers, target = "100 50 5 10 4 5", 579m
//let numbers, target = "50 5 7 9 10 6", 965m

let stringifyItems (items : Item seq) = 
    items 
    |> Seq.map (sprintf "%O")
    |> Seq.reduce (fun a b -> sprintf "%s %s" a b)

let printResults ( (total: num) , items) = sprintf "%0.0f -> %s" total (items |> stringifyItems)
let printResults2 ( (totals: List<num>) , items) = 
    let totalsText = totals |> Seq.map (sprintf "%0.0f" )|> Seq.reduce (sprintf "%s,%s")
    sprintf "%s -> %s" totalsText (items |> stringifyItems)

#time "on" 
//
//numbers 
//|> getAllPerms
//|> Seq.map numbersToItemNumbers
//|> Seq.collect getTotalsForNumberList
//|> Seq.filter (fun (total, _) -> total = target)
////|> Seq.find(fun (total, _) -> total = target) |> PSeq.singleton
//|> Seq.map printResults
//|> Seq.iter (printfn "%A")

numbers 
|> parseStringToStack
|> PGetAllPerms
|> PSeq.map numbersToItemNumbers
|> PSeq.collect getTotalsForNumberList
|> PSeq.filter (fun (totals, _) -> totals |> Seq.exists(fun total -> total = target) )
//|> PSeq.map (fun (totals,items) -> (totals |> Seq.tryFind(fun total -> total = target) ) , items)
//|> PSeq.filter(fun (t, _) -> t.IsSome)
//|> PSeq.map (fun (t,i) -> (t.Value, i) )
//|> PSeq.map printResults
|> PSeq.map printResults2
|> Seq.iter (printfn "%A")

// TODO
// - Integer maths -> should use double or decimal - DONE
// - Less than all the numbers
//      - When executing fall out if total matched?
// - http://ccg.doc.gold.ac.uk/papers/colton_aisb14a.pdf
// Other rules: (From: http://datagenetics.com/blog/august32014/index.html)
// - If at any time the intermediate solution becomes negative, we can instantly bail on that solution.
// - Similarly if the intermediate solution becomes non-integer.

