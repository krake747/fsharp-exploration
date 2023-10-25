open System
open CSharpLib

let person = Person("Kevin", 30)
printfn $"Hello {person.Name}. You are {person.Age} years old"

type MyDisposableType () = 
    interface IDisposable with 
        member _.Dispose() = 
            printfn "Disposing!"

type MyInterface =
    abstract Capitalise : string -> string
    abstract Add : int -> int

type MyImplementation () = 
    interface MyInterface with
        member this.Capitalise text = text.ToUpper()
        member this.Add number = number + 1
        
let implementation: MyInterface = MyImplementation()
let capitalise = implementation.Capitalise "test"

let implementation2 = {
    new MyInterface with 
        member this.Capitalise text = text.ToUpper()
        member this.Add number = number + 1
}

let text = implementation2.Capitalise "test"


