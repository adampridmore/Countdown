module Countdown

open StackCalculator2
open ExpressionTree
open NumbersSolver
open WordSolver
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Http

type NumberSolutionDto = {
    Expression: string
    Result: int
    Score: int
}

type LetterSolutionDto = {
    Word: string
    Length: int
}

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)

    // Add CORS for development
    builder.Services.AddCors(fun options ->
        options.AddDefaultPolicy(fun policy ->
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod() |> ignore)) |> ignore

    let app = builder.Build()

    app.UseCors() |> ignore
    app.UseStaticFiles() |> ignore
    
    // API Endpoints
    app.MapGet("/api/numbers", fun (context: HttpContext) ->
        let req = context.Request
        let targetStr = req.Query.["target"].ToString()
        let numbersStr = req.Query.["numbers"].ToString()

        try
            let target = System.Int32.Parse(targetStr)
            let NumberList = parseStringToStack numbersStr
            
            let bestSolutions = 
                solveBest NumberList target 10
                |> List.map (fun sol -> { 
                    Expression = toInfix sol.Expression
                    Result = sol.Result
                    Score = scoreSolution sol 
                })

            context.Response.WriteAsJsonAsync(bestSolutions)
        with
        | _ -> context.Response.WriteAsJsonAsync({| error = "Invalid parameters" |})
    ) |> ignore

    app.MapGet("/api/letters", fun (context: HttpContext) ->
        let req = context.Request
        let lettersStr = req.Query.["q"].ToString()

        if System.String.IsNullOrWhiteSpace(lettersStr) then
            context.Response.WriteAsJsonAsync [||]
        else
            let words = WordSolver.solve lettersStr |> Seq.toList
            let results = words |> List.map (fun (word, len) -> { Word = word; Length = len })
            context.Response.WriteAsJsonAsync(results)
    ) |> ignore

    // Default route
    app.MapGet("/", fun (context: HttpContext) ->
        context.Response.Redirect("/index.html")
        System.Threading.Tasks.Task.CompletedTask
    ) |> ignore

    app.Run()
    0
