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

let themOrder = {
    Name = "Tom Prime"
    Billing = "12 Heaven Road\nTomorrow\n24665"
    Delivery = ClickAndCollect 2
}

let labels = [ myOrder; hisOrder; herOrder ] |> deliveryLabels

labels |> Seq.iter (fun l -> printfn $"{l}")

let collections =
    [ myOrder; hisOrder; herOrder; themOrder ]
    |> collectionsFor 2
    |> Seq.iter (fun c -> printfn $"{c}")

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

// --- Collections

let houses = [|
    { Address = "1 Acacia Avenue"; Price = 250_000m }
    { Address = "2 Bradley Street"; Price = 380_000m }
    { Address = "1 Carlton Road"; Price = 98_000m }
|]

// Filter (C# Where)

let cheapHouses = houses |> Array.filter (fun h -> h.Price < 100_000m)

cheapHouses |> Array.iter (fun h -> printf $"{h}")

let housePrices =
    House.getRandom 20
    |> Array.map (fun h -> $"Address: {h.Address} - Price: {h.Price}")

housePrices |> Array.iter (fun h -> printfn $"{h}")

let average =
    House.getRandom 20
    |> Array.filter (fun h -> h.Price > 200_000m)
    |> Array.averageBy (_.Price)

printfn $"Average: {average}"

let over2500000 =
    House.getRandom 20
    |> Array.filter (fun h -> h.Price > 250_000m)
    |> Array.sortByDescending _.Price

over2500000 |> Array.iter (fun h -> printfn $"{h}")

let housesNearSchools =
    House.getRandom 20
    |> Array.choose (fun h ->
        match h |> Distance.tryToSchool with
        | Some d -> Some(h, d)
        | None -> None)

housesNearSchools
|> Array.iter (fun (h, d) -> printfn $"House: {h} - Distance {d}")

let singleHouse =
    House.getRandom 20
    |> Array.choose (fun h ->
        match h |> Distance.tryToSchool with
        | Some d -> Some(h, d)
        | None -> None)
    |> Array.find (fun (h, _) -> h.Price < 100_000m)

printfn $"Single House: {fst singleHouse} - Distance: {snd singleHouse}"

let ex1 = [| 10.m; 100.m; 1000.m |] |> Average.averageOrZero
printfn $"{ex1}"

let ex2: decimal = [||] |> Average.averageOrZero
printfn $"{ex2}"

let ex3: decimal = [||] |> Array.averageOr 1.m
printfn $"{ex3}"
