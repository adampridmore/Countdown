module StackCalculator2Tests

open StackCalculator2
open FsUnit
open NUnit.Framework

[<Test>]
let ``1 2 + 3 + = 6``() = 
    [Number(1);Number(2);Operator(+);Number(3);Operator(+)]
    |> execute
    |> should equal 6

[<Test>]
let ``1 2 + 3 * = 9``()= 
    [Number(1);Number(2);Operator(+);Number(3);Operator(*)]
    |> execute
    |> should equal 9

[<Test>]
let ``1 2 * 3 + = 5``()= 
    [Number(1);Number(2);Operator(*);Number(3);Operator(+)]
    |> execute
    |> should equal 5
    
[<Test>]
let ``1 2 * 3 * = 6``()= 
    [Number(1);Number(2);Operator(*);Number(3);Operator(*)]
    |> execute
    |> should equal 6
    
// 1 2 3 4 5
[<Test>]
let ``1 2 + 4 + 5 + = 12``()=
    [Number(1);Number(2);Operator(+);Number(4);Operator(+);Number(5);Operator(+)]
    |> execute
    |> should equal 12

