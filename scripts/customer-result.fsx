type Customer = { Id: int; IsVip: bool; Credit: decimal }

let map f result =
    match result with
    | Ok x -> Ok(f x)
    | Error ex -> Error ex

let bind f result =
    match result with
    | Ok x -> f x
    | Error ex -> Error ex

let getPurchases customer =
    try
        let purchases = if customer.Id % 2 = 0 then (customer, 120m) else (customer, 80m)
        Ok purchases
    with ex ->
        Error ex

let tryPromoteToVip purchases =
    let customer, amount = purchases
    if amount > 100m then { customer with IsVip = true } else customer

let increaseCreditIfVip customer =
    try
        let increase = if customer.IsVip then 100m else 50m
        Ok { customer with Credit = customer.Credit + increase }
    with ex ->
        Error ex

let upgradeCustomer customer =
    customer
    |> getPurchases
    |> Result.map tryPromoteToVip
    |> Result.bind increaseCreditIfVip

let customerVIP = { Id = 1; IsVip = true; Credit = 0.0M }
let customerSTD = { Id = 2; IsVip = false; Credit = 100.0M }

let assertVIP =
    upgradeCustomer customerVIP = Ok { Id = 1; IsVip = true; Credit = 100.0M }

let assertSTDtoVIP =
    upgradeCustomer customerSTD = Ok { Id = 2; IsVip = true; Credit = 200.0M }

let assertSTD =
    upgradeCustomer { customerSTD with Id = 3; Credit = 50.0M } = Ok { Id = 3; IsVip = false; Credit = 100.0M }

printfn $"{assertVIP}"
printfn $"{assertSTDtoVIP}"
printfn $"{assertSTD}"
