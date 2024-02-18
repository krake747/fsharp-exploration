// Abstract Classes
// To convert IFizzBuzz into an abstract class, decorate it with the [<AbstractClass>] attribute.

// Interfaces
type IFizzBuzz =
    abstract member Calculate: int -> string

// Class types
type FizzBuzz(mapping) =
    let calculate n =
        mapping
        |> List.map (fun (v, s) -> if n % v = 0 then s else "")
        |> List.reduce (+)
        |> fun s -> if s = "" then string n else s

    interface IFizzBuzz with
        member _.Calculate(value) = calculate value

let doFizzBuzz mapping range =
    let fizzBuzz = FizzBuzz(mapping) :> IFizzBuzz // Upcast
    range |> List.map fizzBuzz.Calculate

let doFizzBuzzAlt mapping range =
    let fizzBuzz = FizzBuzz(mapping)
    range |> (fizzBuzz :> IFizzBuzz).Calculate

let output =
    doFizzBuzz [ (3, "Fizz"); (5, "Buzz") ] [ 1..15 ]
    |> List.iter (fun x -> printfn $"%A{x}")
