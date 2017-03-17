module GetCombinationsTests

open FsUnit
open NUnit.Framework

open GetCombinations

[<Test>]
let first()= 
    getCombinations 2 2 
    |> Seq.take 1
    |> Seq.last
    |> should equal [0;0]

[<Test>]
let second()= 
    getCombinations 2 2 
    |> Seq.take 2
    |> Seq.last
    |> should equal [1;0]

[<Test>]
let last()= 
    getCombinations 2 2 
    |> Seq.last
    |> should equal [1;1]


[<Test>]
let getCombinations2()= 
    let actual = 
        getCombinations2 2 ["A";"B"] 
        |> Seq.toList

    actual.[0] |> should equal ["A";"A"]
    actual.[1] |> should equal ["B";"A"]
    actual.[2] |> should equal ["A";"B"]
    actual.[3] |> should equal ["B";"B"]
