// Create list of five integers
let items = [ 1..5 ]

// List Comprehension
let itemsA = [
    for x in 1..5 do
        yield x
]

// List Comprehension without yield since > F#5
let itemsB = [
    for x in 1..5 do
        x
]

// Cons operator (add item to list)
let extendedItems = 6 :: items

// Pattern matching list (head and tail)
let readList items =
    match items with
    | [] -> "Empty list"
    | [ head ] -> $"Head: {head}"
    | head :: tail -> $"Head: {head} and Tail: {tail}"

let emptyList = readList [] // "Empty list"
let multipleList = readList [ 1; 2; 3; 4; 5 ] // "Head: 1 and Tail: [2;3;4;5]"
let singleItemList = readList [ 1 ] // "Head: 1"

// Concatenation
let list1 = [ 1..5 ]
let list2 = [ 3..7 ]
let emptyListA = []

let joined = list1 @ list2
let joinedA = List.concat [ list1; list2 ]
let joinedEmpty = list1 @ emptyListA
let emptyJoined = emptyListA @ list1

// Filter (C# Where)
let myList = [ 1..9 ]

let getEvens items =
    items
    |> List.filter (fun x -> x % 2 = 0)
    
let evens = getEvens myList

// Sum
let sum items =
    items |> List.sum
    
let mySum = sum myList

// Map (C# Select)
let triple items =
    items
    |> List.map (fun x -> x * 3)
    
let myTriples = triple [ 1..5 ]

let print items = items |> List.iter (fun x -> printfn $"My value is %i{x}")
print [ 1..9 ]

let tupleItems = [ (1, 0.25M); (5, 0.25M); (1, 2.25M); (1, 125M); (7, 10.9M) ]

// map + sum
let sumMap items =
    items
    |> List.map (fun (q, p) -> decimal q * p)
    |> List.sum
    
let sumMap2 items =
    items
    |> List.sumBy (fun (q, p) -> decimal q * p)
    
let sumMapA = sumMap tupleItems
let sumMapB = sumMap2 tupleItems

// Folding (C# Aggregate)
let folded =
    [ 1..10 ]
    |> List.fold (fun acc v -> acc + v) 0

let getTotal items =
    items |> List.fold (fun acc (q, p) -> acc + decimal q * p) 0M

let getTotal2 items =
    (0M, items) ||> List.fold (fun acc (q, p) -> acc + decimal q * p)
    
let total = getTotal tupleItems
let total2 = getTotal2 tupleItems

// Group and Uniqueness
let ungrouped = [ 1; 2; 3; 4; 5; 7; 6; 5; 4; 3; ]

let gpResult =
    ungrouped |> List.groupBy id

let unique items =
    items
    |> List.groupBy id
    |> List.map fst
    
let unResult =
    unique ungrouped

let uniqueSet items =
    items |> Set.ofList
    
let setResult = uniqueSet ungrouped

// Solving problems in many ways
let nums = [ 1..10 ]

// Step by step
let a =
    nums
    |> List.filter (fun v -> v % 2 = 1)
    |> List.map (fun v -> v * v)
    |> List.sum

// Using option and choose
let b =
    nums
    |> List.choose (fun v -> if v % 2 = 1 then Some (v * v) else None)
    |> List.sum

// Fold
let c =
    nums
    |> List.fold (fun acc v -> acc + if v % 2 = 1 then (v * v) else 0) 0

// Reduce
let d =
    match nums with
    | [] -> 0
    | items -> items |> List.reduce (fun acc v -> acc + if v % 2 = 1 then (v * v) else 0)

// The recommended version
let e =
    nums
    |> List.sumBy (fun v -> if v % 2 = 1 then (v * v) else 0)
