#r @"..\packages\MyFSharpHelpers.1.0.1.0\lib\net461\MyFSharpHelpers.dll"

#load "StackCalculator2.fs"
#load "GetCombinations.fs"
#load "CountdownSolver.fs"

open StackCalculator2
open CountdownSolver
open GetCombinations
open Permutations

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


//let numbers = [100;75;6;5;4] // 744
let numbers = [3; 7; 6; 2; 1; 7] //and a target of 824
let target = 824


// 2, 4, 5, 9, 10 and 100 into a figure of 566.
let numbersToItemNumbers numbers =
    numbers 
    |> Seq.map (fun n->Item.Number(n))


// PGetAllPerms
 
numbers 
|> permutations 
|> Seq.map numbersToItemNumbers
|> Seq.collect getTotalsForNumberList
|> Seq.where (fun total -> total = target)
//|> Seq.find ( (=) target)
//|> Seq.distinct
//|> Seq.length
//|> Seq.iter (printfn "%A")
|> Seq.length
|> printfn "%d"

// TODO
// - Integer maths -> should use double or decimal
// - Less than all the numbers

