module WordSolver

open System

/// Calculate frequency array for characters 'a' to 'z'
let getFrequencies (s: string) : int[] =
    let freqs = Array.zeroCreate 26
    for i in 0 .. s.Length - 1 do
        let c = s.[i]
        match c with
        | c when c >= 'a' && c <= 'z' -> 
            let idx = int c - int 'a'
            freqs.[idx] <- freqs.[idx] + 1
        | c when c >= 'A' && c <= 'Z' ->
            let idx = int c - int 'A'
            freqs.[idx] <- freqs.[idx] + 1
        | _ -> () // ignore non-alphabetic
    freqs

/// Check if a word can be formed using the given letter frequencies
let isMatch (lettersFreq: int[]) (word: string) : bool =
    let mutable possible = true
    let mutable i = 0
    let len = word.Length
    
    // We clone the frequencies so we can decrement them
    let remainingFreq = Array.copy lettersFreq
    
    while possible && i < len do
        let c = word.[i]
        let idx = 
            if c >= 'a' && c <= 'z' then int c - int 'a'
            elif c >= 'A' && c <= 'Z' then int c - int 'A'
            else -1
            
        if idx >= 0 then
            remainingFreq.[idx] <- remainingFreq.[idx] - 1
            if remainingFreq.[idx] < 0 then
                possible <- false
                
        i <- i + 1
    
    possible

/// Legacy wrapper for tests that directly call isMatch with two strings
let isMatchStr (letters: string) (word: string) : bool =
    let lettersFreq = getFrequencies letters
    isMatch lettersFreq word

let solve (letters: string) : seq<string * int> = 
  
  let wordsFileName = "words_alpha.txt"

  let words = System.IO.File.ReadAllLines(wordsFileName)

  let lettersFreq = getFrequencies letters

  words
  |> Seq.filter (fun word -> isMatch lettersFreq word)
  |> Seq.map( fun word -> (word, word.Length))
  |> Seq.sortBy(snd)
  