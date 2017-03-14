module PolishEvaluatorTests

open FsUnit
open NUnit.Framework
open PolishEvaluator

[<Test>]
let ``+ 1 2 = 3``() = 
    [Operator(Plus);Number(1); Number(2)] |> evaluateItems |> should equal 3

[<Test>]
let ``+ 1 * 2 3 = 7``() =
    // + 1 * 2 3 
    // 1 + (2*3) = 7
    [Operator(Plus);Number(1); Operator(Multiply); Number(2);Number(3)] 
    |> evaluateItems 
    |> should equal 7

[<Test>]
let ``Just 1``() = 
    [Number(1)] |> evaluateItems |> should equal 1

[<Test>]
let ``+ is invalid``() =    
    try
        [Operator(Plus)] |> evaluateItems |> ignore
        NUnit.Framework.Assert.Fail("Exception not thrown")
    with
    | :? System.Exception as e -> e.Message |> should equal "Items cannot start with just one operator"

    