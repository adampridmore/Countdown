﻿#load "StackCalculator2.fs"
#load "CountdownSolver.fs"
#load "GetCombinations.fs"

open StackCalculator2
open CountdownSolver
open GetCombinations

// 1 2 + 3 + = 6 
// 1 2 + 3 * = 9
// 1 2 * 3 + = 5
// 1 2 * 3 * = 6

// http://happysoft.org.uk/countdown/numgame.php
//let numbers = [Number(1);Number(2);Number(3);Number(4);Number(5);Number(6)]
//let numbers = [Number(75);Number(100);Number(3);Number(4);Number(5);Number(6)]
//let numbers = [Number(75);Number(100);Number(4);Number(5);Number(6)]
let numbers = [Number(75);Number(100);Number(5);Number(6);Number(4)]
//let numbers = ["75";"100";"5";"6";"4"]
//let operators = ["+";"-";"*";"/"]

numbers 
|> getTotalsForNumberList
|> Seq.iter (printfn "%A")
