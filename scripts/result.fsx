open System

let tryDivide (x: decimal) (y: decimal) =
    try
        Ok(x / y)
    with :? DivideByZeroException as ex ->
        Error ex

let badDivide = tryDivide 1m 0m
let goodDivide = tryDivide 2m 1m

let printFnResult result =
    match result with
    | Ok v -> printfn $"Ok {v}"
    | Error e -> printfn $"Error {e}"

printFnResult badDivide
printFnResult goodDivide
