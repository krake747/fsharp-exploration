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
        if someValue < 10.0 then
            calculateGroup 15
        else
            calculateGroup 35

    "Hello " + group

let result = sayHello 10.5
printfn $"{result}"

// Unit as an input
let getTheCurrentTime () = DateTime.Now
let now = getTheCurrentTime ()
let yesterday = (getTheCurrentTime ()).AddDays(-1)

printfn $"{now} and {yesterday}"

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
    if theAddress.Country = "UK" then
        {
            Name = "David", "Beckham"
            Address = theAddress
        }
    else
        {
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

let pipeline = 10 |> add 5 |> add 7 |> multiply 2

printfn $"Pipeline result is {pipeline}"

let addFiveAndDouble input = input |> add 5 |> multiply 2

let addFiveAndDoubleShort = add 5 >> multiply 2

// Higher Order Functions
let executeCalculation operation name a b =
    let answer = operation a b
    printfn $"{name} {a} and {b} = {answer}"
    answer

let add5And6 = executeCalculation add "add" 5 6
let multiply5And6 = executeCalculation multiply "multiply" 5 6

// let writeToFile text =
//     IO.File.AppendAllText("log.txt", text)
//
// let calculation = executeCalculation writeToFile 10 20

// Functional Collection pipelines
type Result = {
    HomeTeam: string
    HomeGoals: int
    AwayTeam: string
    AwayGoals: int
}

type TeamSummary = { Name: string ; mutable AwayWins: int }

let create home hg away ag = {
    HomeTeam = home
    HomeGoals = hg
    AwayTeam = away
    AwayGoals = ag
}

let results = [
    create "Messiville" 1 "Ronaldo City" 2
    create "Messiville" 1 "Bale Town" 3
    create "Ronaldo City" 2 "Bale Town" 3
    create "Bale Town" 2 "Messiville" 1
]

let isAwayWin result = result.AwayGoals > result.HomeGoals

let wonAwayTheMost =
    results
    |> List.filter isAwayWin
    |> List.countBy (fun result -> result.AwayTeam)
    |> List.sortByDescending (fun (team, count) -> count)
    |> List.head

let ronaldoCityCount =
    results
    |> List.filter (fun x -> x.AwayTeam = "Ronaldo City" || x.HomeTeam = "Ronaldo City")
    |> List.length

let scoredMost =
    results
    |> List.collect (fun x -> [
        {|
            Team = x.HomeTeam
            Goals = x.HomeGoals
        |}
        {|
            Team = x.AwayTeam
            Goals = x.AwayGoals
        |}
    ])
    |> List.groupBy (fun x -> x.Team)
    |> List.map (fun (team, games) -> team, games |> List.sumBy (fun game -> game.Goals))
    |> List.sortByDescending snd

let userInput =
    seq {
        printfn "Enter command (X to exit)"

        while true do
            Console.ReadKey().KeyChar
    }

let processInputCommands commands =
    commands
    |> Seq.takeWhile (fun cmd -> cmd <> 'x')
    |> Seq.iter (fun cmd ->
        printfn ""

        if cmd = 'w' then printfn "Withdrawing money!"
        elif cmd = 'd' then printfn "Depositing money!"
        else printfn $"You executed command {cmd}")

userInput |> processInputCommands