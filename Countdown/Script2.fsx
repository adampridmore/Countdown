#load "StackCalculator2.fs"
open StackCalculator2

// 1 2 + 3 + = 6
[Number(1);Number(2);Operator(+);Number(3);Operator(+)]
|> execute

// 1 2 + 3 * = 9
[Number(1);Number(2);Operator(+);Number(3);Operator(*)]
|> execute

// 1 2 * 3 + = 5
[Number(1);Number(2);Operator(*);Number(3);Operator(+)]
|> execute

// 1 2 * 3 * = 6 
[Number(1);Number(2);Operator(*);Number(3);Operator(*)]
|> execute

// 1 2 3 4 5
// 1 2 + 4 + 5 +
[Number(1);Number(2);Operator(+);Number(4);Operator(+);Number(5);Operator(+)]
|> execute
