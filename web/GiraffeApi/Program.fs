open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Giraffe.EndpointRouting
open Giraffe.ViewEngine

let indexView =
    html [] [
        head [] [ title [] [ str "Giraffe Api" ] ]
        body [] [
            h1 [] [ str "I |> F#" ]
            p [ _class "some-css-class"; _id "someId" ] [ str "Hello World from the Giraffe View Engine" ]
        ]
    ]

let sayHelloNameHandler (name: string) : HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) -> {| Response = $"Hello {name}, how are you?" |} |> ctx.WriteJsonAsync

let apiRoutes = [
    GET [
        route "" (json {| Response = "Hello world!!" |})
        routef "/%s" sayHelloNameHandler
    ]
]

let endpoints = [ GET [ route "/" (htmlView indexView) ]; subRoute "/api" apiRoutes ]

let notFoundHandler = "Not Found" |> text |> RequestErrors.notFound

let configureServices (services: IServiceCollection) =
    services.AddRouting().AddGiraffe() |> ignore

let configureApp (appBuilder: IApplicationBuilder) =
    appBuilder.UseRouting().UseGiraffe(endpoints).UseGiraffe(notFoundHandler)

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
