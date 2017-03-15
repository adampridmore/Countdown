module StackCalculatorTests

open FsUnit
open NUnit.Framework
open StackCalculator

[<Test>]
let ``1 2 + = 3``() = 
    START 
    |> ONE |> TWO |> ADD 
    |> CALCULATE
    |> should equal 3

[<Test>]
let ``2 3 * = 6``() = 
    START 
    |> TWO |> THREE |> MUL
    |> CALCULATE
    |> should equal 6

[<Test>]
let ``6 2 / = 3``() = 
    START 
    |> SIX |> TWO |> DIV
    |> CALCULATE
    |> should equal 3

[<Test>]
let ``4 3 - = 1``() = 
    START 
    |> FOUR |> THREE|> SUB
    |> CALCULATE
    |> should equal 1
