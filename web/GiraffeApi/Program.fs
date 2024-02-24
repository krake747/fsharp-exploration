open GiraffeApi.TodoStore
open Microsoft.AspNetCore.Builder

open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Giraffe.EndpointRouting
open Giraffe.ViewEngine
open GiraffeApi

let indexView =
    html [] [
        head [] [ title [] [ str "Giraffe Todo Api" ] ]
        body [] [
            h1 [] [ str "I |> F#" ]
            p [] [ str "Hello Todo Web API from the Giraffe View Engine" ]
        ]
    ]

let apiRoutes = [ GET [ route "" (json {| Response = "Hello world!!" |}) ] ]

let endpoints = [
    GET [ route "/" (htmlView indexView) ]
    subRoute "api/todo" Todos.apiTodoRoutes
    subRoute "/api" apiRoutes
]

let notFoundHandler = "Not Found" |> text |> RequestErrors.notFound

let configureServices (services: IServiceCollection) =
    services
        .AddRouting()
        .AddGiraffe()
        .AddSingleton(TodoStore())
    |> ignore

let configureApp (appBuilder: IApplicationBuilder) =
    appBuilder
        .UseRouting()
        .UseGiraffe(endpoints)
        .UseGiraffe(notFoundHandler)

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    configureServices builder.Services

    let app = builder.Build()

    if app.Environment.IsDevelopment() then
        app.UseDeveloperExceptionPage() |> ignore

    configureApp app

    // app.MapGet("/", Func<string>(fun () -> "Hello World!")) |> ignore

    app.Run()

    0 // Exit code
