type RegisteredCustomer = { Id: string; IsEligible: bool }

type UnregisteredCustomer = { Id: string }

type Customer =
    | Registered of RegisteredCustomer
    | Guest of UnregisteredCustomer
    
let sarah = Guest { Id = "Sarah" }
let john = Registered { Id = "John"; IsEligible = true }
let mary = Registered { Id = "Mary"; IsEligible = true }
let richard = Registered { Id = "Richard"; IsEligible = false }

let calculateTotal customer spend =
    let discount =
        match customer with
        | Registered c -> if c.IsEligible && spend >= 100.0m then spend * 0.1m else 0.0m
        | Guest _ -> 0.0m
    
    spend - discount



let areEqual expected actual =
    actual = expected

let assertJohn = areEqual 90.0m (calculateTotal john 100.0m)
let assertMary = areEqual 99.0m (calculateTotal mary 99.0m)
let assertRichard = areEqual 100.0m (calculateTotal richard 100.0m)
let assertSarah = areEqual 100.0m (calculateTotal sarah 100.0m)

printfn $"{assertJohn}"
printfn $"{assertMary}"
printfn $"{assertRichard}"
printfn $"{assertSarah}"
