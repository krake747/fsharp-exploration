module MyProject.Customer

type Customer = { Id: int; IsVip: bool; Credit: decimal }

let getPurchases customer =
    let purchases = if customer.Id % 2 = 0 then 120m else 80m
    customer, purchases

let tryPromoteToVip purchases =
    let customer, amount = purchases
    if amount > 100m then { customer with IsVip = true } else customer

let increaseCreditIfVip customer =
    let increase = if customer.IsVip then 100m else 50m
    { customer with Credit = customer.Credit + increase }

let upgradeCustomer customer =
    customer |> getPurchases |> tryPromoteToVip |> increaseCreditIfVip

let customerVIP = { Id = 1; IsVip = true; Credit = 0.0m }
let customerSTD = { Id = 2; IsVip = false; Credit = 100.0m }

let areEqual expected actual = actual = expected

let assertVIP =
    let expected = { Id = 1; IsVip = true; Credit = 100.0m }
    areEqual expected (upgradeCustomer customerVIP)

let assertSTDtoVIP =
    let expected = { Id = 2; IsVip = true; Credit = 200.0m }
    areEqual expected (upgradeCustomer customerSTD)

let assertSTD =
    let expected = { Id = 3; IsVip = false; Credit = 100.0m }
    areEqual expected (upgradeCustomer { Id = 3; IsVip = false; Credit = 50.0m })
