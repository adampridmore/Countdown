module StackCalculator2Tests

open StackCalculator2
open FsUnit
open NUnit.Framework

[<Test>]
let ``1 2 + 3 + = 6``() = 
    [Number(1);Number(2);Plus;Number(3);Plus]
    |> execute
    |> should equal 6

[<Test>]
let ``1 2 + 3 * = 9``()= 
    [Number(1);Number(2);Plus;Number(3);Multiply]
    |> execute
    |> should equal 9

[<Test>]
let ``1 2 * 3 + = 5``()= 
    [Number(1);Number(2);Multiply;Number(3);Plus]
    |> execute
    |> should equal 5
    
[<Test>]
let ``1 2 * 3 * = 6``()= 
    [Number(1);Number(2);Multiply;Number(3);Multiply]
    |> execute
    |> should equal 6
    
// 1 2 3 4 5
[<Test>]
let ``1 2 + 4 + 5 + = 12``()=
    [Number(1);Number(2);Plus;Number(4);Plus;Number(5);Plus]
    |> execute
    |> should equal 12

[<Test>]
let broken() = 
    [Number(1);Number(2);Plus;Number(3);Plus]
    |> execute
    |> should equal 6