module BikeStore.Common.ResultExtensions

let failOnError result =
    match result with
    | Ok success -> success
    | Error error -> failwithf $"{error}"
