module Tests

open System
open Xunit

open WordSolver

let LettersAssert letters word expectedResult : Unit =
  let actualResult : bool = isMatch letters word

  if (actualResult = expectedResult) then
    ()
  else
    let error = $"Expected match: %b{expectedResult} %s{word} to match %s{letters}"
    raise (Exception(error))

[<Fact>]
let ``Is Match When no Match``() =
  let letters = "abc"
  let word = "cheese"

  LettersAssert letters word false

[<Fact>]
let ``Is Match When Exact Match``() =
  let letters = "abc"
  let word = "abc"

  LettersAssert letters word true

[<Fact>]
let ``Is Match When All Letters Exact Match``() =
  let letters = "abc"
  let word = "cba"
  LettersAssert letters word true

[<Fact>]
let ``Is Match When All Letters Match``() =
  let letters = "abcd"
  let word = "cba"
  LettersAssert letters word true

[<Fact>]
let ``Is Match When All Letters Match with some spare``() =
  let letters = "aabc"
  let word = "cba"
  LettersAssert letters word true

[<Fact>]
let ``Is Match When not match``() =
  let letters = "a"
  let word = "b"
  LettersAssert letters word false

[<Fact>]
let ``Real test``() =
  let letters = "zorosmofrksw"
  let word = "workrooms"
  LettersAssert letters word true

[<Fact>]
let ``Search for best match``() =
  solve("NOGROIIAS")
  |> Seq.iter(fun (word, length) -> (printfn "Answer: %s (%d)" word length))

