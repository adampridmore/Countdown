module ExpressionTree

open StackCalculator2

/// Binary expression tree for arithmetic expressions
type Expr =
    | Num of decimal
    | BinOp of Expr * Operator * Expr

/// A single calculation step
type Step = {
    Left: decimal
    Op: string
    Right: decimal
    Result: decimal
}

/// Evaluate an expression tree, returning None if any intermediate result violates game rules
let evaluate (expr: Expr) : decimal option =
    let rec eval = function
        | Num n -> Some n
        | BinOp(left, op, right) ->
            match eval left, eval right with
            | Some l, Some r when r <> 0m || op.name <> "/" ->
                let result = op.fn l r
                if isValid result then Some result else None
            | _ -> None
    eval expr

/// Evaluate an expression tree and collect all intermediate steps
let evaluateWithSteps (expr: Expr) : (decimal option * Step list) =
    let steps = ResizeArray<Step>()

    let rec eval = function
        | Num n -> Some n
        | BinOp(left, op, right) ->
            match eval left, eval right with
            | Some l, Some r when r <> 0m || op.name <> "/" ->
                let result = op.fn l r
                if isValid result then
                    steps.Add({ Left = l; Op = op.name; Right = r; Result = result })
                    Some result
                else None
            | _ -> None

    let result = eval expr
    (result, steps |> Seq.toList)

/// Get operator precedence for bracket placement
let private precedence (op: Operator) =
    match op.name with
    | "+" | "-" -> 1
    | "*" | "/" -> 2
    | _ -> 0

/// Check if the parent operator requires right associativity handling
let private isRightAssociativeIssue (parentOp: Operator) =
    parentOp.name = "-" || parentOp.name = "/"

/// Convert an expression tree to human-readable infix notation with clarifying parentheses
let toInfix (expr: Expr) : string =
    let rec toInfixImpl (expr: Expr) (parentOp: Operator option) (isRight: bool) : string =
        match expr with
        | Num n -> sprintf "%.0f" n
        | BinOp(left, op, right) ->
            let inner = sprintf "%s %s %s"
                            (toInfixImpl left (Some op) false)
                            op.name
                            (toInfixImpl right (Some op) true)
            match parentOp with
            | None -> inner
            | Some pOp ->
                let needsParens =
                    // Always add parens when precedence differs (clarifying)
                    precedence op <> precedence pOp ||
                    // Still handle right-associativity issues for same precedence
                    (isRight && precedence op = precedence pOp && isRightAssociativeIssue pOp)
                if needsParens then sprintf "(%s)" inner else inner

    toInfixImpl expr None false

/// Convert steps to a step-by-step string representation
let toStepByStep (steps: Step list) : string =
    steps
    |> List.mapi (fun i step ->
        sprintf "  %d. %.0f %s %.0f = %.0f" (i + 1) step.Left step.Op step.Right step.Result)
    |> String.concat "\n"
