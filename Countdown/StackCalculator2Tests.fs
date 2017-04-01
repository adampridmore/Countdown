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

[<Test>]
let ``toString for Number``()= 
    (Number(1m)).ToString() |> should equal "1"

[<Test>]
let ``toString for Operator``()= 
    (Plus).ToString() |> should equal "+"

[<Test>]
let ``parse string to stack`` ()=
    let stack = "1 2 +" |> parseStringToStack |> Seq.toList
    
    let toDecimal = 
            function
            | Number(x) -> Some(x)
            | Operator(_) -> None 

    let numbers = 
        stack 
        |> Seq.choose toDecimal 
        |> Seq.toList

    numbers.[0] |> should equal (1m)
    numbers.[1] |> should equal (2m)
    stack.[2] |> should equal Plus


//[<Test>]
//let ``bail out if negative``() = 
//    [Number(1m);Number(2m);Minus]
//    |> execute2
//    |> should equal Invalid

//[<Test>]
//let ``Drop out if found solution before end``()=
//    [Number(1m);Number(2m]

