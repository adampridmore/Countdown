module StackCalculator2Tests

open StackCalculator2
open FsUnit
open Xunit

[<Fact>]
let ``1 2 + 3 + = 6``() = 
    [Number(1m);Number(2m);Plus;Number(3m);Plus]
    |> execute
    |> should equal 6m

[<Fact>]
let ``1 2 + 3 * = 9``()= 
    [Number(1m);Number(2m);Plus;Number(3m);Multiply]
    |> execute
    |> should equal 9m

[<Fact>]
let ``1 2 * 3 + = 5``()= 
    [Number(1m);Number(2m);Multiply;Number(3m);Plus]
    |> execute
    |> should equal 5m
    
[<Fact>]
let ``1 2 * 3 * = 6``()= 
    [Number(1m);Number(2m);Multiply;Number(3m);Multiply]
    |> execute
    |> should equal 6m
    
// 1 2 3 4 5
[<Fact>]
let ``1 2 + 4 + 5 + = 12``()=
    [Number(1m);Number(2m);Plus;Number(4m);Plus;Number(5m);Plus]
    |> execute
    |> should equal 12m

[<Fact>]
let ``toString for Number``()= 
    (Number(1m)).ToString() |> should equal "1"

[<Fact>]
let ``toString for Operator``()= 
    (Plus).ToString() |> should equal "+"

[<Fact>]
let ``parse string to stack`` ()=
    "1 2" |> parseStringToStack 
    |> should equal [1m;2m]

// 1 2 3 4 5
[<Fact>]
let ``1 2 + 4 + 5 + = 12   (execute2)``()=
    [Number(1m);Number(2m);Plus;Number(4m);Plus;Number(5m);Plus]
    |> execute2
    |> should equal ([12m;7m;3m])


[<Fact>]
let ``Stop on negative``()=
    [Number(1m);Number(2m);Minus]
    |> execute2
    |> should equal ( List.empty<decimal>)

[<Fact>]
let ``Stop on fraction``()=
    [Number(1m);Number(2m);Divide]
    |> execute2
    |> should equal ( List.empty<decimal>)


