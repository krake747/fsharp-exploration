open System

type PersonName = {
    FirstName: string
    MiddleName: string option
    LastName: string
}

// Reference type
let nullObj: string = null

// Nullable type
let nullPri = Nullable<int>()

let tryParseDateTime (input: string) =
    match DateTime.TryParse input with
    | true, result -> Some result
    | _ -> None

// To convert from .Net to an F# Option type
let fromNullObj = Option.ofObj nullObj
let fromNullPri = Option.ofNullable nullPri

// To convert from an Option type to .Net types
let toNullObj = Option.toObj fromNullObj
let toNullPri = Option.toNullable fromNullPri

let printFnValue x =
    match x with
    | Some s -> printfn $"{s}"
    | _ -> printfn "None"

let printFnValue2 x =
    match x with
    | null -> printfn "Null"
    | _ -> printfn "String"

let printFnValue3 (x: Nullable<int>) =
    match x with
    | x when x.HasValue = true -> printfn $"{x.Value}"
    | _ -> printfn "Null"

printFnValue fromNullObj
printFnValue fromNullPri
printFnValue2 toNullObj
printFnValue3 toNullPri

let resultPM input =
    match input with
    | Some value -> value
    | None -> "------"

let resultDV = Option.defaultValue "------" fromNullObj
let resultFP = fromNullObj |> Option.defaultValue "------"
