# Countdown Solver - Specification

## Background

This project solves puzzles from the UK TV gameshow "Countdown". The show features three types of puzzles, two of which are implemented here.

---

## Problem 1: Letters Round

### The Problem

Given a set of 9 letters (a mix of vowels and consonants), find the longest valid English word that can be spelled using only those letters. Each letter can only be used once.

**Example:**
- Letters: `B U C T E A S D N`
- Best answer: `EDUCANTS` (8 letters)

### Constraints

- Words must exist in a standard English dictionary
- Each letter can only be used as many times as it appears in the input
- Longer words score more points

### Dictionary Note

The actual TV show uses the **Oxford Dictionary of English** as its official reference, with adjudicators in "Dictionary Corner" checking contested words. This dictionary is not publicly available as a standalone word list.

This implementation uses `words_alpha.txt`, a general English word list (~370k words). For more authentic results, alternatives include:
- **SOWPODS** - The Scrabble word list used in UK/international tournaments
- **Collins Scrabble Words (CSW)** - Another competitive word game dictionary

### How It's Solved

**Algorithm:** Dictionary filtering with letter frequency matching

1. Load a dictionary of valid English words (~370k words from `words_alpha.txt`)
2. For each word in the dictionary:
   - Sort both the available letters and the word's letters
   - Use a greedy recursive match: if letters match, consume both; if not, skip the available letter
   - If all word letters are consumed, the word is valid
3. Return all valid words sorted by length (longest first)

**Key insight:** Sorting both inputs allows a single linear pass to determine validity, handling duplicate letters correctly (e.g., "aardvark" needs two 'a's).

---

## Problem 2: Numbers Round

### The Problem

Given 6 numbers and a 3-digit target, find an arithmetic expression using some or all of the numbers that equals the target. Only addition, subtraction, multiplication, and division are allowed.

**Example:**
- Numbers: `25, 8, 6, 9, 7, 2`
- Target: `682`
- Solution: `(25 + 6) * (8 + (7 * 2)) = 682`

### Constraints (Game Rules)

- Each number can only be used once
- Not all numbers need to be used
- All intermediate results must be positive integers
- Division must be exact (no fractions or remainders)
- No negative intermediate values allowed

### How It's Solved

**Algorithm:** Expression tree generation with exhaustive search

1. **Generate all subsets** of the input numbers (must use at least 2)
2. **Generate all permutations** of each subset
3. **Generate all binary expression trees** for each permutation:
   - For n numbers, generate all possible tree structures (bracketing patterns)
   - For each structure, try all 4 operators at each node
4. **Evaluate each tree**, stopping early if any intermediate result violates game rules
5. **Filter** expressions that reach the target
6. **Rank solutions** by human-friendliness:
   - Fewer operations (steps) is better
   - Avoid division (harder to compute mentally)
   - Prefer "nice" intermediate results (multiples of 10, 25, 100)
7. **Normalize** equivalent expressions (e.g., `6 + 25` becomes `25 + 6`)
8. **Return top 5** unique solutions

**Key insight:** Using binary expression trees (not RPN) allows finding solutions that require non-left-to-right evaluation, like `2 * (3 + 4)`, which a simple left-to-right calculator would miss.

---

## Problem 3: Conundrums Round

### The Problem

Unscramble a 9-letter anagram to find a single valid word.

### Status

**Not implemented.** Could potentially reuse the Letters Round solver with an exact 9-letter constraint.

---

## Output Formatting

### Clarifying Parentheses

Expressions are displayed with parentheses that clarify evaluation order, even when not strictly necessary by precedence rules:

- Instead of: `7 + 25 * 6 * 9 / 2`
- Shows: `7 + (25 * 6 * 9 / 2)`

### Canonical Ordering

For commutative operations (+ and *), larger operands are placed on the left:

- Instead of: `6 + 25`
- Shows: `25 + 6`

This reduces duplicate solutions and matches how humans typically express calculations.

---

## Technology

- **Language:** F# (.NET 8.0)
- **Testing:** xUnit + FsUnit
- **Key data structures:** Discriminated unions, lazy sequences, binary expression trees
