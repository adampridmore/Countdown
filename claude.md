# Countdown Solver - Codebase Analysis

## Overview

This is an F# solution for solving puzzles from the UK TV gameshow "Countdown". The project implements solvers for **two of the three main game types**: the Letters Round and the Numbers Round.

**Technology Stack:**
- .NET 8.0
- F# (functional programming)
- xUnit + FsUnit (testing)

---

## Project Structure

```
Countdown/
├── Countdown.sln                 # Solution file
├── countdown.fsproj              # F# project (targeting .NET 8.0)
├── readme.md                     # Usage documentation
│
├── CORE MODULES:
│   ├── Program.fs                # CLI entry point
│   ├── WordSolver.fs             # Letters round solver
│   ├── StackCalculator2.fs       # RPN arithmetic calculator
│   ├── CountdownSolver.fs        # Numbers round orchestrator
│   ├── GetCombinations.fs        # Combinatorial generator
│   └── permutations.fs           # Permutation generator
│
├── TESTS:
│   ├── WordSolverTests.fs        # Letter matching tests
│   ├── StackCalculator2Tests.fs  # RPN calculator tests
│   ├── GetCombinationsTests.fs   # Combination generation tests
│   └── CountdownSolverTests.fs   # Number solver tests
│
└── WORD LISTS:
    ├── words_alpha.txt           # Primary dictionary (~370k words, 3.8MB)
    └── corncob_lowercase.words   # Alternative word list (543KB, unused)
```

---

## Implemented Features

### Letters Round (`WordSolver.fs`)

Finds the longest words that can be formed from a set of letters.

**Algorithm:**
1. Loads word dictionary from `words_alpha.txt`
2. For each word, checks if it can be formed using available letters
3. Uses a recursive matching algorithm that sorts both inputs and greedily matches characters
4. Returns results sorted by word length (longest first)

**Key Function:**
```fsharp
let isMatch (letters: string) (word: string) : bool
```
- Sorts both letter sets and word characters
- Recursively matches, allowing unused letters
- Correctly handles letter frequency (e.g., needs two 'a's to spell "aardvark")

**CLI Usage:**
```bash
dotnet run -l bucteasdn
# Output: educants, andesite, ... (sorted by length)
```

### Numbers Round (`CountdownSolver.fs` + `StackCalculator2.fs`)

Finds arithmetic expressions that reach a target number using given numbers and the four basic operators.

**Algorithm:**
1. Generate all permutations of the input numbers
2. For each permutation, generate all operator combinations (+, -, *, /)
3. Interleave numbers and operators into RPN (Reverse Polish Notation) sequences
4. Evaluate using a stack-based calculator
5. Filter results that reach the target and follow game rules

**Game Rules Enforced:**
- All intermediate results must be positive integers
- Division must be exact (no fractions)
- No negative intermediate values

**Key Components:**

| Module | Responsibility |
|--------|----------------|
| `StackCalculator2.fs` | RPN evaluation engine with validation |
| `CountdownSolver.fs` | Orchestrates permutations + operator combinations |
| `Permutations.fs` | Generates all orderings of numbers |
| `GetCombinations.fs` | Generates all operator combinations |

**CLI Usage:**
```bash
dotnet run -n "25 8 6 9 7 2" 682
# Output: Solutions showing arithmetic to reach 682
```

### Not Implemented

- **Conundrums Round** - The 9-letter anagram bonus round is not implemented

---

## Architecture & Design

### Module Dependency Graph

```
Program.fs (CLI)
    │
    ├── WordSolver.fs (letters game)
    │
    └── CountdownSolver.fs (numbers game)
            │
            ├── StackCalculator2.fs (RPN calculator)
            ├── Permutations.fs (orderings)
            └── GetCombinations.fs (operator combos)
```

### Key Design Decisions

1. **Discriminated Unions for Type Safety**
   ```fsharp
   type Item =
     | Number of num
     | Operator of Operator
   ```
   Prevents mixing numbers and operators incorrectly in the stack.

2. **Decimal Arithmetic**
   ```fsharp
   type num = decimal
   ```
   Ensures precise arithmetic without floating-point errors.

3. **Lazy Sequences**
   Uses F# `seq` expressions throughout to handle the combinatorial explosion efficiently without materializing all results in memory.

4. **Two Calculator Modes**
   - `execute`: Simple evaluation returning final result
   - `execute2`: Returns intermediate results and stops on invalid operations (crucial for game rules)

---

## F# Patterns & Techniques

### Recursive Sequence Generators

```fsharp
let rec permutations l =
    seq {
        if List.length l = 1 then yield l
        else
            for sub in permutations l.Tail do
                yield! insertions [] l.Head sub
    }
```

### Pattern Matching with Guards

```fsharp
match (remainingLetters, remainingWord) with
| (letter::tailLetters), (wordLetter::tailWord) when letter = wordLetter ->
    test tailLetters tailWord
| (_::tailLetters), (word) -> test tailLetters word
| ([], _::_) -> false
| _, [] -> true
```

### Structured Format Display

```fsharp
[<StructuredFormatDisplay("{name}")>]
type Operator = {
    fn : (num -> num -> num)
    name : string
}
```

### Functional Composition

```fsharp
split >> Seq.map parse >> Seq.toList
```

---

## Testing

The project uses **xUnit** with **FsUnit** for idiomatic F# assertions.

| Test File | Coverage |
|-----------|----------|
| `WordSolverTests.fs` | Letter matching edge cases, real-world examples |
| `StackCalculator2Tests.fs` | RPN evaluation, stopping conditions, parsing |
| `GetCombinationsTests.fs` | Combination generation correctness |
| `CountdownSolverTests.fs` | Operator/number merging |

Run tests with:
```bash
dotnet test
```

---

## Build & Run

```bash
# Build
dotnet build

# Run tests
dotnet test

# Run CLI
dotnet run -l <letters>           # Letters round
dotnet run -n "<numbers>" <target> # Numbers round
```

---

## Potential Improvements

1. **Add Conundrums Solver** - Could reuse `WordSolver` with exact 9-letter constraint
2. **Performance Optimization** - Could add early termination when exact solution found
3. **Parallel Processing** - The permutation/combination space could be parallelized
4. **Alternative Dictionary** - The `corncob_lowercase.words` file exists but isn't used
5. **Solution Ranking** - Could rank solutions by simplicity (fewer operations)

---

## Summary

A well-structured, idiomatic F# implementation demonstrating:
- Functional decomposition and modularity
- Type-safe design with discriminated unions
- Efficient lazy evaluation for combinatorial problems
- Clean pattern matching and recursive algorithms
- Comprehensive test coverage

The codebase is maintainable, extensible, and follows F# best practices.