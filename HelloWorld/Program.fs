open System

let greetPerson (name: string) (time: DateTime) =
    $"Hello from F# func, {name}. It is {time}"

let name = "Kevin"
let time = DateTime.Now

let greeting = greetPerson name time

printfn $"{greeting}"

// Type inference
let add a b =
    let answer = a + b
    answer
    
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
let drive gas distance =
    if distance > 50 then gas / 2.0
    elif distance > 25 then gas - 10.0
    elif distance > 0 then gas - 1.0
    else gas

let gas = 100.0 
let firstState = drive gas 55
let secondState = drive firstState 36
let finalState = drive secondState 5
  
printfn $"Gas: {finalState}"

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