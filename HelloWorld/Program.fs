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
    let group = if someValue < 10.0 then calculateGroup 15 else calculateGroup 35

    "Hello " + group

let result = sayHello 10.5
printfn $"{result}"

// Unit as an input
let getTheCurrentTime () = DateTime.Now
let now = getTheCurrentTime ()
let yesterday = (getTheCurrentTime ()).AddDays(-1)

printfn $"{now} and {yesterday}"

// Records
type Address = { Line1: string; Line2: string; City: string; Country: string }

type Person = { Name: string * string; Address: Address }

let theAddress = { Line1 = "1st Street"; Line2 = "Apt. 1"; City = "London"; Country = "UK" }

let addressInDE = { theAddress with City = "Berlin"; Country = "DE" }

let generatePerson theAddress =
    if theAddress.Country = "UK" then
        { Name = "David", "Beckham"; Address = theAddress }
    else
        { Name = "John", "Doe"; Address = theAddress }

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
type Result = { HomeTeam: string; HomeGoals: int; AwayTeam: string; AwayGoals: int }

type TeamSummary = { Name: string; mutable AwayWins: int }

let create home hg away ag = { HomeTeam = home; HomeGoals = hg; AwayTeam = away; AwayGoals = ag }

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
        {| Team = x.HomeTeam; Goals = x.HomeGoals |}
        {| Team = x.AwayTeam; Goals = x.AwayGoals |}
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

// userInput |> processInputCommands

// Pattern Matching
let value = ConsoleModifiers.Alt

let description =
    match value with
    | ConsoleModifiers.Alt -> "Alt was pressed."
    | ConsoleModifiers.Control -> "You hit control!"
    | ConsoleModifiers.Shift -> "Shift held down..."
    | _ -> "Some modifier was pressed"


type CustomerDetails = { YearsOfHistory: int; HasOverdraft: bool; Overdraft: decimal }

let customerDetailsRecord = { YearsOfHistory = 1; HasOverdraft = true; Overdraft = 400m }

let overdraftLimit (customerDetails: CustomerDetails) =
    (customerDetails.YearsOfHistory |> decimal) * 250.0m > customerDetails.Overdraft

let canTakeOutALoanRecord =
    let hasLargeOverdraft = overdraftLimit customerDetailsRecord

    match customerDetailsRecord with
    | { YearsOfHistory = 0 } -> false
    | { YearsOfHistory = 1; HasOverdraft = true } when hasLargeOverdraft = true -> false
    | { YearsOfHistory = 2; HasOverdraft = true } when hasLargeOverdraft = true -> false
    | { YearsOfHistory = 2; HasOverdraft = false } -> true
    | _ -> true

printfn $"Customer is eligible for loan {canTakeOutALoanRecord}"
// Look at sth called 'Active Patterns' to simplify more complex matching

type OverdraftDetails = { Approved: bool; MaxAmount: decimal; CurrentAmount: decimal }

type CustomerAddress = { Country: string }

type CustomerWithOverdraft = { YearsOfHistory: int; Overdraft: OverdraftDetails; Address: CustomerAddress }

let canTakeOutALoanRecursive (customer: CustomerWithOverdraft) =
    match customer with
    | {
          YearsOfHistory = 0 | 1
          Overdraft = { Approved = true }
          Address = { Country = "US" }
      } -> true
    | { YearsOfHistory = 0 | 1 } -> false
    | { Address = { Country = "US" } } -> true
    | _ -> true

let canTakeOutALoanBinding (customer: CustomerWithOverdraft) =
    match customer with
    | { Overdraft = { Approved = true; CurrentAmount = amount } } ->
        printfn $"Loan approved; current overdraft is {amount}"
        true
    | { Overdraft = { Approved = true } as overdraftDetails } ->
        printfn $"Loan approved; overdraft details are {overdraftDetails}"
        true
    | _ -> false

// Collection matching
type LoanRequest = {
    YearsOfHistory: int
    HasOverdraft: bool
    LoanRequestAmount: decimal
    IsLargeRequest: bool
}

let summariseLoanRequests requests =
    match requests with
    | [] -> "No requests made!"
    | [ { IsLargeRequest = true } ] -> "Single large request!"
    | [ { IsLargeRequest = true }; { IsLargeRequest = true } ] -> "Two large requests"
    | { IsLargeRequest = false } :: remainingItems -> "Several"
    | _ :: { HasOverdraft = true } :: _ -> "Second item has an overdraft"
    | _ -> "Anything else"

// Discriminated Union

type TelephoneNumber =
    | Local of number: string
    | International of countryCode: string * number: string

type ContactMethod =
    | Email of address: string
    | Telephone of country: string * number: string
    | Post of {| Line1: string; Line2: string; City: string; Country: string |}
    | Sms of TelephoneNumber

type Customer = { Name: string; Age: int; ContactMethod: ContactMethod }

let smsContact = Sms(Local "123-4567")
let smsInternationalContact = Sms(International("+365", "123-4567"))
let isaacEmail = Email "isaac@myemailaddress.com"
let isaacPhone = Telephone("UK", "020-8123-4567")

let isaacPost =
    Post {|
        Line1 = "1 The Street"
        Line2 = "On Baker's Street"
        City = "Munich"
        Country = "DE"
    |}

let message = "Discriminated Unions FTW!"
let customer = { Name = "Isaac"; Age = 24; ContactMethod = isaacPost }

let fancyMessage =
    match customer.ContactMethod with
    | Email address -> $"Emailing '{message}' to {address}."
    | Telephone(country, number) -> $"Calling {country}-{number} with the message '{message}'!"
    | Post postDetails -> $"Printing a letter with contents '{message}' to {postDetails}"
    | Sms number -> $"Sms '{message}' to {number}."

printfn $"{fancyMessage}"

let sendTo customer message =
    match customer.ContactMethod with
    | Email address -> failwith "todo"
    | Telephone(country, number) -> failwith "todo"
    | Post foo -> failwith "todo"
    | Sms(International(code, intNumber)) -> $"Texting local number {code}{intNumber}"
    | Sms(Local number) -> $"Texting local number {number}"

type PhoneNumber = PhoneNumber of string
type CountryCode = CountryCode of string

type TelephoneNumberRevised =
    | Local of PhoneNumber
    | International of CountryCode * PhoneNumber

let localNumber = Local(PhoneNumber "123-456")

let internationalNumber =
    let countryCode = CountryCode "+44"
    let phoneNumber = PhoneNumber "208-123-4567"
    International(countryCode, phoneNumber)

type Email = Email of address: string
type ValidatedEmail = ValidatedEmail of Email

let validateEmail (Email address) =
    if not (address.Contains "@") then failwith "Invalid email" else ValidatedEmail(Email address)

type Programmer = {
    Username: string
    Age: int
}

let krake747 = {
    Username = "krake747"
    Age = 30
}

let getAge programmer =
    programmer.Age
    
let classifyAge age =
    match age with
    | age when age < 18 -> "Child"
    | age when age < 65 -> "Adult"
    | _ -> "Senior"
    
let optionalClassification =
    Some krake747
    |> Option.map getAge
    |> Option.map classifyAge
    
printfn $"{optionalClassification}"

type CustomerId = CustomerId of string
type Name = Name of string
type Street = Street of string
type City = City of string
type Country = Domestic | Foreign of string
type AccountBalance = AccountBalance of decimal

type RawCustomer = {
    CustomerId : string
    Name : string
    Street : string
    City : string
    Country : string
    AccountBalance : decimal
}

type Customer2 = {
    Id : CustomerId
    Name : Name
    Address : {|
        Street : Street
        City : City
        Country : Country
    |}
    Balance : AccountBalance
}

type CustomerValidationError2 =
    | InvalidCustomerId of string
    | InvalidName of string
    | InvalidCountry of string
    
type CustomValidationFunc = string -> Result<CustomerId, CustomerValidationError2>
    
let validateCustomerId (cId:string) =
    if cId.StartsWith "C" then Ok (CustomerId cId[1..])
    else Error (InvalidCustomerId $"Invalid Customer Id '{cId}'.") 

type CustomerIdError =
    | EmptyId
    | InvalidIdFormat
    
type NameError =
    | NoNameSupplied
    | TooManyParts
type CountryError =
    | NoCountrySupplied
type CustomerValidationError =
    | CustomerIdError of CustomerIdError
    | NameError of NameError
    | CountryError of CountryError
    
type ValidateCustomerId = string -> Result<CustomerId, CustomerIdError> 
type ValidateName = string -> Result<CustomerId, NameError> 
type ValidateCountry = string -> Result<CustomerId, CountryError> 
type ValidationRawCustomer = RawCustomer -> Result<Customer, CustomerValidationError>
