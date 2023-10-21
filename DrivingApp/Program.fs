open System

printfn "Hello Driving App"

printfn "How far do you want to drive?"
let parsed, distance = Console.ReadLine() |> Int32.TryParse

let startGas = 8.0
let remainingGas = startGas |> Car.drive distance

if remainingGas.IsOutOfGas then printfn "You are out of gas!"
else printfn $"You have {remainingGas.GasLeft} gas left."
