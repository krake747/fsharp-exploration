open System

let greetPerson (name: string) (time: DateTime) =
    $"Hello from F# func, {name}. It is {time}"

let name = "Kevin"
let time = DateTime.Now

let greeting = greetPerson name time

printfn $"{greeting}"

// Type inference   
let calculateGroup age = 
    if age < 18 then "Child"
    elif age < 65 then "Adult"
    else "Pensioner"
    
let sayHello someValue =
    let group =
        if someValue < 10.0 then calculateGroup 15
        else calculateGroup 35
    "Hello " + group
    
let result = sayHello 10.5
printfn $"{result}"

// Unit as an input
let getTheCurrentTime () = DateTime.Now 
let now = getTheCurrentTime ()
let yesterday = (getTheCurrentTime ()).AddDays(-1)

printfn $"{now} and {yesterday}"

// Mutable state with mutable variables
let drive distance gas =
    if distance > 50 then gas / 2.0
    elif distance > 25 then gas - 10.0
    elif distance > 0 then gas - 1.0
    else gas

let gas = 100.0

let remainingGas =
    gas
    |> drive 55
    |> drive 26
    |> drive 1

printfn $"Gas: {remainingGas}"

// Records
type Address = {
    Line1: string
    Line2: string
    City: string
    Country: string
}

type Person = {
    Name: string * string
    Address: Address
}

let theAddress = {
    Line1 = "1st Street"
    Line2 = "Apt. 1"
    City = "London"
    Country = "UK"
}
let addressInDE = {
    theAddress with
        City = "Berlin"
        Country = "DE" 
}

let generatePerson theAddress =
    if theAddress.Country = "UK" then {
        Name = "David", "Beckham"
        Address = theAddress 
    }
    else {
        Name = "John", "Doe"
        Address = theAddress 
    }
        
let person = generatePerson theAddress
printfn $"{person}"

// Currying and Partial Application

let add a b = a + b
let multiply a b = a * b    
    
let addFive = add 5
let res = addFive 10
printfn $"{res}"

let pipeline =
    10
    |> add 5
    |> add 7
    |> multiply 2

printfn $"Pipeline result is {pipeline}"