type CustomerId = CustomerId of string

type RegisteredCustomer = { Id: CustomerId }

type UnregisteredCustomer = { Id: CustomerId }

type ValidationError = InputOutOfRange of string

[<Struct>]
type Spend = private Spend of decimal

module Spend =

    let value input = input |> fun (Spend value) -> value

    let create input =
        if input >= 0.0M && input <= 1000.0M then
            Ok(Spend input)
        else
            Error(InputOutOfRange "You can only spend between 0 and 1000")

type Total = decimal

type DiscountPercentage = decimal

type Customer =
    | Eligible of RegisteredCustomer
    | Registered of RegisteredCustomer
    | Guest of UnregisteredCustomer

module Customer =
    let calculateDiscountPercentage (spend: Spend) (customer: Customer) : DiscountPercentage =
        match customer with
        | Eligible _ -> if Spend.value spend >= 100.0M then 0.1M else 0.0M
        | _ -> 0.0M

    let calculateTotal (customer: Customer) (spend: Spend) : Total =
        customer
        |> calculateDiscountPercentage spend
        |> fun discountPercentage -> Spend.value spend * (1.0M - discountPercentage)

// type CalculateTotal = Customer -> Spend -> Total

// let calculateTotalAlt: CalculateTotal =
//     fun customer spend ->
//         let discount =
//             match customer with
//             | Eligible _ when spend >= 100.0M -> spend * 0.1M
//             | _ -> 0.0M
//         spend - discount

let john = Eligible { Id = CustomerId "John" }
let mary = Eligible { Id = CustomerId "Mary" }
let richard = Registered { Id = CustomerId "Richard" }
let sarah = Guest { Id = CustomerId "Sarah" }

let isEqualTo expected actual = actual = expected

let assertEqual customer spent expected =
    Spend.create spent
    |> Result.map (Customer.calculateTotal customer)
    |> isEqualTo (Ok expected)

let assertJohn = assertEqual john 100.0M 90.0M |> printfn "%A"

let assertMary = assertEqual mary 99.0M 99.0M |> printfn "%A"

let assertRichard = assertEqual richard 100.0M 100.0M |> printfn "%A"

let assertSarah = assertEqual sarah 100.0M 100.0M |> printfn "%A"
