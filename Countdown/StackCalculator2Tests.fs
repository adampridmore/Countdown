module StackCalculator2Tests

open StackCalculator2
open FsUnit
open NUnit.Framework

[<Test>]
let ``1 2 + 3 + = 6``() = 
    [Number(1m);Number(2m);Plus;Number(3m);Plus]
    |> execute
    |> should equal 6m

[<Test>]
let ``1 2 + 3 * = 9``()= 
    [Number(1m);Number(2m);Plus;Number(3m);Multiply]
    |> execute
    |> should equal 9m

[<Test>]
let ``1 2 * 3 + = 5``()= 
    [Number(1m);Number(2m);Multiply;Number(3m);Plus]
    |> execute
    |> should equal 5m
    
[<Test>]
let ``1 2 * 3 * = 6``()= 
    [Number(1m);Number(2m);Multiply;Number(3m);Multiply]
    |> execute
    |> should equal 6m
    
// 1 2 3 4 5
[<Test>]
let ``1 2 + 4 + 5 + = 12``()=
    [Number(1m);Number(2m);Plus;Number(4m);Plus;Number(5m);Plus]
    |> execute
    |> should equal 12m

[<Test>]
let broken() = 
    [Number(1m);Number(2m);Plus;Number(3m);Plus]
    |> execute
    |> should equal 6m
