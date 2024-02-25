open GiraffeApi.TodoStore
open Microsoft.AspNetCore.Builder

open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Giraffe.EndpointRouting
open Giraffe.ViewEngine
open GiraffeApi

let indexView =
    [
        h1 [] [ str "I |> F#" ]
        p [ _class "some-css-class"; _id "someId" ] [ str "Hello Todo Web API from the Giraffe View Engine" ]
    ]
    |> Shared.masterPage "Giraffe View Engine"

let apiRoutes = [ GET [ route "" (json {| Response = "Hello world!!" |}) ] ]

let endpoints = [
    GET [ route "/" (htmlView (Todos.Views.todoView Todos.Data.todoList)) ]
    subRoute "api/todo" Todos.apiTodoRoutes
    subRoute "/api" apiRoutes
]

let notFoundHandler = "Not Found" |> text |> RequestErrors.notFound

let configureServices (services: IServiceCollection) =
    services.AddRouting().AddGiraffe().AddSingleton(TodoStore()) |> ignore

let configureApp (appBuilder: IApplicationBuilder) =
    appBuilder
        .UseRouting()
        .UseStaticFiles()
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

    let store = app.Services.GetRequiredService<TodoStore>()
    Todos.Data.todoList |> List.map store.Create |> ignore
    
    // app.MapGet("/", Func<string>(fun () -> "Hello World!")) |> ignore

    app.Run()

    0 // Exit code
