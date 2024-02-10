type Customer = { Id: int; IsVip: bool; Credit: decimal }

let getPurchases customer =
    let purchases = if customer.Id % 2 = 0 then 120m else 80m
    customer, purchases

let tryPromoteVip purchases =
    let customer, amount = purchases
    if amount > 100m then { customer with IsVip = true } else customer

let increaseCreditIfVip customer =
    let increase = if customer.IsVip then 100m else 50m
    { customer with Credit = customer.Credit + increase }

// Composition operator
let upgradeCustomerComposed = getPurchases >> tryPromoteVip >> increaseCreditIfVip

// Nested
let upgradeCustomerNested customer =
    increaseCreditIfVip (tryPromoteVip (getPurchases customer))

// Procedural
let upgradeCustomerProcedural customer =
    let customerWithPurchases = getPurchases customer
    let promotedCustomer = tryPromoteVip customerWithPurchases
    let increasedCreditCustomer = increaseCreditIfVip promotedCustomer
    increasedCreditCustomer

// Forward pipe operator
let upgradeCustomerPiped customer =
    customer |> getPurchases |> tryPromoteVip |> increaseCreditIfVip


let customerVIP = { Id = 1; IsVip = true; Credit = 0m }
let customerSTD = { Id = 2; IsVip = false; Credit = 100m }

let assertVIP =
    upgradeCustomerComposed customerVIP = { Id = 1; IsVip = true; Credit = 100m }

let assertSTDtoVIP =
    upgradeCustomerComposed customerSTD = { Id = 2; IsVip = true; Credit = 200m }

let assertSTD =
    upgradeCustomerComposed { customerSTD with Id = 3; Credit = 50m } = { Id = 3; IsVip = false; Credit = 100m }

printfn $"{assertVIP}"
printfn $"{assertSTDtoVIP}"
printfn $"{assertSTD}"
