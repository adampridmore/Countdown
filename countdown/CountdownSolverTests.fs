module CountdownSolverTests

open FsUnit
open Xunit

open CountdownSolver

[<Fact>]
let ``Merge operators into numbers``()=
    let numbers = [1;2;3;4]
    let operators = [0;0;0]
    
    mergeOperatorsIntoNumbers numbers operators 
    |> Seq.toList
    |> should equal [1;2;0;3;0;4;0]
