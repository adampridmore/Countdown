#load "PolishEvaluator.fs"
open PolishEvaluator

//let n = [1;2;3]

//let apply (a,b) =
//    [(a + b);
//    (a - b);
//    (a / b);
//    (a * b)]
//
//apply (1,2)
   
// + 1 * 2 3 


//1 2 3
//1 + (2 * 3)         + 1 * 2 3
//1 + (2 + 3)         
//1 * (2 * 3)
//1 * (2 + 3)
//(1 + 2) + 3         + 1 2 + 3
//(1 + 2) * 3         * + 1 2 3
//(1 * 2) + 3
//(1 * 2) * 3

// 3 + 1 2 
[Operator(Multiply);Number(3);Operator(Plus);Number(1);Number(2)]
|> evaluateItems

//  * 3 + 1 2 
[Operator(Multiply);Number(3);Operator(Plus);Number(1);Number(2)]
|> evaluateItems


// × (− 5 6) 7
[Operator(Multiply);Operator(Minus);Number(5);Number(6);Number(7)]
|> evaluateItems

// https://gist.github.com/MorleyDev/3e4ca64e0f4f61a4bdf0
// https://fsharpforfunandprofit.com/posts/stack-based-calculator/
