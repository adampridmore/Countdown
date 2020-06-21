module GetCombinationsTests

open FsUnit
open Xunit

open GetCombinations

[<Fact>]
let first()= 
    getCombinations 2 2 
    |> Seq.take 1
    |> Seq.last
    |> Seq.toList
    |> should equal [0;0]
    // |> printfn "%O"

[<Fact>]
let second()= 
    getCombinations 2 2 
    |> Seq.take 2
    |> Seq.last
    |> Seq.toList    
    |> should equal [1;0]

[<Fact>]
let last()= 
    getCombinations 2 2 
    |> Seq.last
    |> Seq.toList
    |> should equal [1;1]


[<Fact>]
let getCombinations2()= 
    let actual = 
        getCombinations2 2 ["A";"B"] 
        |> Seq.toList

    actual.[0] |> Seq.toList |> should equal ["A";"A"]
    actual.[1] |> Seq.toList |> should equal ["B";"A"]
    actual.[2] |> Seq.toList |> should equal ["A";"B"]
    actual.[3] |> Seq.toList |> should equal ["B";"B"]
