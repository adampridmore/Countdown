#load "StackCalculator2.fs"

open StackCalculator2

//[Number(1m);Number(2m);Plus;Number(4m);Plus]
//|> execute2
//
//[Number(1m);Number(2m);Minus]
//|> execute2

[Number(1m);Number(2m);Divide]
|> execute2

[Number(1m);Number(2m);Plus;Number(4m);Plus;Number(5m);Plus;Number(2m);Multiply]
|> execute2
