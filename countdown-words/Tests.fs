module Tests

open System
open System.IO
open Xunit

let isMatch(letters: String) (word: String) : bool =
  let sortedLetters = letters.ToCharArray() |>  Array.toList |> List.sort
  let wordSorted = word.ToCharArray() |> Array.toList |> List.sort

  let rec test(remainingLetters: List<char>) (remainingWord: List<Char>) : bool =
    match (remainingLetters, remainingWord) with
    | (letter::tailLetters), (wordLetter::tailWord) when letter = wordLetter -> test tailLetters tailWord
    | (letter::tailLetters), (wordLetter::tailWord) when letter <> wordLetter -> test tailLetters (wordLetter::tailWord)
    | ([],wordLetter::tail) -> false
    | _,[] -> true
    | letters, word -> raise (Exception($"Unexpected pattern match: Letters: %A{letters} Word:%A{word}"))
    
  test sortedLetters wordSorted

let LettersAssert letters word expectedResult : Unit =
  let actualResult : bool = isMatch letters word

  if (actualResult = expectedResult) then
    ()
  else
    let error = $"Expected match: %b{expectedResult} %s{word} to match %s{letters}"
    raise (Exception(error))

// [<Fact>]
// let ``test loading file`` () =
//   let words = File.ReadAllLines("corncob_lowercase.words")

//   printfn "%d" (words.Length)

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
  let letters = "zorosmofrksw"

  let words = File.ReadAllLines("corncob_lowercase.words")

  words
  |> Seq.filter (fun word -> isMatch letters word)
  |> Seq.map( fun word -> (word, word.Length))
  |> Seq.sortBy(snd)
  |> Seq.iter(fun (word, length) -> (printfn "Answer: %s (%d)" word length))

