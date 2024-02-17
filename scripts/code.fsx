type ValidationError = InputOutOfRange of string

[<Struct>]
type Spend =
    private
    | Spend of decimal

    member this.Value = this |> fun (Spend value) -> value

    static member Create input =
        if input >= 0.0M && input <= 1000.0M then
            Ok(Spend input)
        else
            Error(InputOutOfRange "You can only spend between 0 and 1000")

type Total = decimal

type RegisteredCustomer = { Id: string }

type UnregisteredCustomer = { Id: string }

type Customer =
    | Eligible of RegisteredCustomer
    | Registered of RegisteredCustomer
    | Guest of UnregisteredCustomer

    member this.CalculateDiscountPercentage(spend: Spend) =
        match this with
        | Eligible _ ->
            if spend.Value >= 100.0M then 0.1M else 0.0M
        | _ -> 0.0M

type CalculateTotal = Customer -> Spend -> Total

let calculateTotal (customer: Customer) (spend: Spend) : Total =
    spend.Value * (1.0M - customer.CalculateDiscountPercentage spend)


// let calculateTotalAlt: CalculateTotal =
//     fun customer spend ->
//         let discount =
//             match customer with
//             | Eligible _ when spend >= 100.0M -> spend * 0.1M
//             | _ -> 0.0M
//         spend - discount

let john = Eligible { Id = "John" }
let mary = Eligible { Id = "Mary" }
let richard = Registered { Id = "Richard" }
let sarah = Guest { Id = "Sarah" }

let isEqualTo expected actual = actual = expected

let assertEqual customer spent expected =
    Spend.Create spent
    |> Result.map (calculateTotal customer)
    |> isEqualTo (Ok expected)

let assertJohn = assertEqual john 100.0M 90.0M |> printfn "%A"

let assertMary = assertEqual mary 99.0M 99.0M |> printfn "%A"

let assertRichard = assertEqual richard 100.0M 100.0M |> printfn "%A"

let assertSarah = assertEqual sarah 100.0M 100.0M |> printfn "%A"
