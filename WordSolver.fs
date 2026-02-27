module WordSolver

open System

let isMatch(letters: String) (word: String) : bool =
  let sortedLetters = letters.ToCharArray() |>  Array.toList |> List.sort
  let wordSorted = word.ToCharArray() |> Array.toList |> List.sort

  let rec test(remainingLetters: List<char>) (remainingWord: List<Char>) : bool =
    match (remainingLetters, remainingWord) with
    | (letter::tailLetters), (wordLetter::tailWord) when letter = wordLetter -> test tailLetters tailWord
    | (_::tailLetters), (word) -> test tailLetters (word)
    | ([] , _::_) -> false
    | _,[] -> true
    
  test sortedLetters wordSorted

let solve (letters: string) : seq<string * int> = 
  
  let wordsFileName = "words_alpha.txt"

  let words = System.IO.File.ReadAllLines(wordsFileName)

  words
  |> Seq.filter (fun word -> word.Length > 3 && isMatch (letters.ToLower()) word)
  |> Seq.map( fun word -> (word, word.Length))
  |> Seq.sortByDescending(snd)