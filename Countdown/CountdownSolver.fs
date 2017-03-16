module CountdownSolver

open StackCalculator2


let operatorCombinationsSeq =
    let operators = [Operator(+);Operator(*);Operator(-);Operator(/)]
    seq {
        for ``1`` in operators do 
            for ``2`` in operators do 
                for ``3`` in operators do 
                    for ``4`` in operators do 
                        yield [``1``;``2``;``3``;``4``]
//                        for ``5`` in operators do 
//                            yield [``1``;``2``;``3``;``4``;``5``]
    }

