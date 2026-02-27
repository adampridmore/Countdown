module StackCalculator2Tests

open StackCalculator2
open FsUnit
open Xunit

[<Fact>]
let ``1 2 + 3 + = 6``() =
    [Number 1;Number 2;Plus;Number 3;Plus]
    |> execute
    |> should equal 6

[<Fact>]
let ``1 2 + 3 * = 9``()=
    [Number 1;Number 2;Plus;Number 3;Multiply]
    |> execute
    |> should equal 9

[<Fact>]
let ``1 2 * 3 + = 5``()=
    [Number 1;Number 2;Multiply;Number 3;Plus]
    |> execute
    |> should equal 5

[<Fact>]
let ``1 2 * 3 * = 6``()=
    [Number 1;Number 2;Multiply;Number 3;Multiply]
    |> execute
    |> should equal 6

// 1 2 3 4 5
[<Fact>]
let ``1 2 + 4 + 5 + = 12``()=
    [Number 1;Number 2;Plus;Number 4;Plus;Number 5;Plus]
    |> execute
    |> should equal 12

[<Fact>]
let ``toString for Number``()=
    (Number 1).ToString() |> should equal "1"

[<Fact>]
let ``toString for Operator``()=
    Plus.ToString() |> should equal "+"

[<Fact>]
let ``parse string to stack`` ()=
    "1 2" |> parseStringToStack
    |> should equal [1;2]

// 1 2 3 4 5
[<Fact>]
let ``1 2 + 4 + 5 + = 12   (execute2)``()=
    [Number 1;Number 2;Plus;Number 4;Plus;Number 5;Plus]
    |> execute2
    |> should equal [12;7;3]


[<Fact>]
let ``Stop on negative``()=
    [Number 1;Number 2;Minus]
    |> execute2
    |> should equal List.empty<int>

[<Fact>]
let ``Stop on fraction``()=
    [Number 1;Number 2;Divide]
    |> execute2
    |> should equal List.empty<int>
