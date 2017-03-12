#load "PolishEvaluator.fs"
open PolishEvaluator

let n = [1;2;3]

let apply (a,b) =
    [(a + b);
    (a - b);
    (a / b);
    (a * b)]

apply (1,2)
   
[Operator(Plus);Number(1); Number(2)] |> evaluateItems 

// + 1 * 2 3 
// 1 + (2*3) = 7
[Operator(Plus);Number(1); Operator(Multiply); Number(2);Number(3)] |> evaluateItems 

[Number(1)] |> evaluateItems

// Invalid
//[Operator(Plus)] |> evaluateItems

