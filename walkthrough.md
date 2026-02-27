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
Evaluated replacing string character sorting and recursive pattern matching with pre-calculated character frequency histograms.

## Results
- Evaluated performance locally on a tough 12-letter search (`zorosmofrksw` vs $370,000$ words).
- **Original Algorithm Time**: $550 \text{ms}$
- **Histogram Algorithm Time**: $490 \text{ms}$
- **Speedup**: ~**11%** faster execution speed.

## Conclusion 
The original `WordSolver.isMatch` approach allocated character arrays and dynamically sorted them via `List.sort` for every word in the dictionary. A frequency histogram approach successfully removed those allocations.

However, since analyzing all 370k words currently completes in barely half a second (the bulk of which is actually I/O file reading speeds), the ~11% functional execution speedup wasn't deemed significant enough to warrant complicating the codebase. The histogram logic has been **reverted** in favor of maintaining the simpler, cleaner recursive pattern matcher.
