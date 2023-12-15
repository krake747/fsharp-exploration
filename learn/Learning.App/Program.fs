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
    Delivery = None
}

let hisOrder = {
    Name = "John Doe"
    Billing = "314 Pi Avenue\nErewhon\n15926"
    Delivery = Some "16 Planck Parkway\nErewhon\n62291"
}

 // None
printfn $"{myOrder |> tryHub |> failOnNone}"
 // Some "Hub 1"
printfn $"{hisOrder |> tryHub |> failOnNone}"
