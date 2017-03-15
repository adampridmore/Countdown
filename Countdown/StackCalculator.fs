module StackCalculator

type Stack = StackContents of float list

let push x (StackContents contents) =   
    StackContents (x::contents)

let newStack = StackContents [1.0;2.0;3.0]

let EMPTY = StackContents []
let ONE = push 1.0
let TWO = push 2.0
let THREE = push 3.0
let FOUR = push 4.0
let FIVE = push 5.0
let SIX = push 6.0

let pop (StackContents contents) = 
    match contents with 
    | top::rest -> (top,rest |> StackContents)
    | [] -> failwith "Stack underflow"

let binary mathFn stack = 
    let a,stack' = pop stack    
    let b,stack'' = pop stack'  
    let result = mathFn b a
    push result stack''   

let ADD = binary (+)
let SUB = binary (-)
let MUL = binary (*)
let DIV = binary (/)

let SHOW stack = 
    let x,_ = pop stack
    printfn "The answer is %f" x
    stack  // keep going with same stack

let CALCULATE stack = 
    let x, _ = pop stack
    x

let START  = EMPTY
