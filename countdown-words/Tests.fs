module Tests

open System
open System.IO
open Xunit

[<Fact>]
let ``My test`` () =
  Assert.True(true)

let ``Asssrt`` letters word expectedResult Unit = 
  true

[<Fact>]
let ``My test 2`` () =
  let words = File.ReadAllLines("corncob_lowercase.words")

  let letters = "zoning"

  printfn "%d" (words.Length)

let isMatch(letters: String) (word: String) : bool =
  let sortedLetters = letters.ToCharArray() |>  Array.toList |> List.sort
  let wordSorted = word.ToCharArray() |> Array.toList |> List.sort

  // sortedLetters = wordSorted

  let rec test(remainingLetters: List<char>) (remainingWord: List<Char>) : bool =
    match (remainingLetters, remainingWord) with
    | (letter::tailLetters), (wordLetter::tailWord) when letter = wordLetter -> test tailLetters tailWord
    | (letter::_,[]) -> false
    | _,[] -> true
    
  test sortedLetters wordSorted

[<Fact>]
let ``Is Match When no Match``() =
  let letters = "abc"
  let word = "cheese"

  Assert.False(isMatch letters word)

[<Fact>]
let ``Is Match When Exact Match``() =
  let letters = "abc"
  let word = "abc"
  Assert.True(isMatch letters word)

[<Fact>]
let ``Is Match When All Letters Exact Match``() =
  let letters = "abc"
  let word = "cba"
  Assert.True(isMatch letters word)

[<Fact>]
let ``Is Match When All Letters Match``() =
  let letters = "abcd"
  let word = "cba"
  Assert.True(isMatch letters word)

// [<Fact>]
// let ``Is Match When All Letters Match with some spare``() =
//   let letters = "aabc"
//   let word = "cba"
//   Assert.True(isMatch letters word)

// [<Fact>]
// let ``Is Match When not match``() =
//   let letters = "a"
//   let word = "b"
//   Assert.False(isMatch letters word)

// [<Fact>]
// let ``Real test``() =
//   let letters = "zorosmofrksw"
//   let word = "workrooms"
//   Assert.True(isMatch letters word)

// [<Fact>]
// let ``Search for best match``() =
//   let letters = "zorosmofrksw"

//   let words = File.ReadAllLines("corncob_lowercase.words")

//   words
//   |> Seq.filter (fun word = isMatch letters word)
//   |> Seq.iter (printfn "Answer: $s")

