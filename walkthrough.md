# Number Solver Performance Tuning
## Overview
Replaced the brute-force enumeration of binary expression trees with a Dynamic Programming approach over subset bitmasks.

## Results
- Evaluated performance locally on the standard 6-number countdown problem (`25 8 6 9 7 2 -> 682`).
- **Original Algorithm Time**: $33,023 \text{ms}$
- **DP Algorithm Time**: $675 \text{ms}$
- **Speedup**: ~$\mathbf{49\times}$ faster!

## Explanation of Strategy
The original `evaluateWithSteps` was called separately on *every* valid binary tree topology of every permutation. This means millions of duplicate or invalid sub-trees (like fractional division) were generated and then discarded. 

By building solutions bottom-up with bitmasks representing exactly which input elements were used:
1. We only calculate valid operations using sub-expressions that are already proven valid (e.g. integer division).
2. We memoize the results using that bitmask, inherently deduping identical arithmetic from identical lists of inputs.

The test `Numbers solver performance` passes successfully and shows these exact numbers.

---

# Letter Solver Performance Tuning

## Overview
Replaced string character sorting and recursive pattern matching with pre-calculated character frequency histograms.

## Results
- Evaluated performance locally on a tough 12-letter search (`zorosmofrksw` vs $370,000$ words).
- **Original Algorithm Time**: $550 \text{ms}$
- **Histogram Algorithm Time**: $490 \text{ms}$
- **Speedup**: ~**11%** faster execution speed.

## Explanation of Strategy
The original `WordSolver.isMatch` allocated a char array, converted it to an F# list, and ran `List.sort` on *every single dictionary word* and the player's letters.

By pre-calculating the character frequencies (a 26-slot `int[]`) for the player's letters *once*, we can evaluate whether any word in the dictionary is an anagram match simply by looping over the word's characters and subtracting from the frequency array without any heap allocations or sorting per word. 
While $550 \text{ms}$ was already surprisingly fast to filter out 370k words, removing the sorting operations dropped the execution directly down to $490 \text{ms}$. Since the IO Read of `words_alpha.txt` makes up $\sim 300 \text{ms}$ of that runtime, the *actual* filtering algorithm speed improved dramatically under the hood!
