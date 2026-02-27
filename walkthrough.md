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
