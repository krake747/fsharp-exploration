open System
open Learning.Domain
open Learning.Domain.Billing

let failOnNone (option: 'a option) =
    match option with
    | Some some -> some
    | _ -> Unchecked.defaultof<'a>

let failOnError result =
    match result with
    | Ok success -> success
    | Error error -> failwithf $"{error}"

let milesPointYards = 1.057
let res = MilesYards.fromMilesPointYards milesPointYards
let decRes = MilesYards.toDecimalMiles res

printfn $"{res} | {decRes}"

// ---

let myOrder = {
    Name = "Kit Eason"
    Billing = "112 Fibonacci Street\nErehwon\n35813"
    Delivery = AsBilling
}

let hisOrder = {
    Name = "John Doe"
    Billing = "314 Pi Avenue\nErewhon\n15926"
    Delivery = Physical "16 Planck Parkway\nErewhon\n62291"
}

let herOrder = {
    Name = "Jane Smith"
    Billing = "9 Gravity Road\nErewhon\n80665"
    Delivery = Download
}

let labels = [ myOrder; hisOrder; herOrder ] |> deliveryLabels

labels |> Seq.iter (fun l -> printfn $"{l}")

// --- Nullable reference type interop C# to F#

let myApiFunction (stringParam: string) =
    let s = stringParam |> Option.ofObj |> Option.defaultValue "(none)"

    printfn $"{s.ToUpper()}"

myApiFunction "hello"
myApiFunction null

// --- Nullable value type interop C# to F#

let showHeartRate (rate: Nullable<int>) =
    rate
    |> Option.ofNullable
    |> Option.map (_.ToString())
    |> Option.defaultValue "N/A"

printfn $"{showHeartRate (Nullable(96))}"
printfn $"{showHeartRate (Nullable())}"

// --- Option type interop F# to C#

//Similar to TrParse
let tryLocationDescription (locationId: int, description: string byref) : bool =
    let r = Random.Shared.Next(1, 100)

    if r < 50 then
        description <- $"Location number %i{r}"
        true
    else
        description <- null
        false

let tryLocationDescription2 (locationId: int) =
    let r = Random.Shared.Next(1, 100)
    if r < 50 then Some $"Location number {r}" else None

let tryLocationDescriptionNullable (locationId: int) =
    tryLocationDescription2 locationId |> Option.toObj

printfn $"{tryLocationDescriptionNullable 99}"

let getHeartRateInternal () =
    let rate = Random.Shared.Next(0, 200)
    if rate = 0 then None else Some rate

let tryGetHeartRate () =
    getHeartRateInternal () |> Option.toNullable

// --- ValueOption

let valueOptionString (v: int voption) =
    match v with
    | ValueSome x -> $"Value: {x}"
    | ValueNone -> "No value"

printfn $"{ValueOption.ValueNone |> valueOptionString}"
printfn $"{ValueOption.ValueSome 99 |> valueOptionString}"
