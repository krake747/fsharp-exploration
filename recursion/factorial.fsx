// two cases:
// a base case, in this example where n equals 1, at which point the recursion ends.
// a general case of greater than 1 which is recursive.
let rec factNoTail n =
    match n with
    | 1 -> 1
    | _ -> n * factNoTail (n - 1)

// Tail Call Optimisation
// Leave public interface intact
// Create enclosed function to do the recursion and use an accumulator
let fact n =
    let rec loop n acc =
        match n with
        | 1 -> acc
        | _ -> loop (n - 1) (acc * n)

    loop n 1

let rec fibNoTail (n: int64) =
    match n with
    | 0L -> 0L
    | 1L -> 1L
    | s -> fibNoTail (s - 1L) + fibNoTail (s - 2L)

// fibNoTail 20 |> printfn "%A"

// Tail Call Optimisation
let fib (n: int64) =
    let rec loop n (a, b) =
        match n with
        | 0L -> a
        | 1L -> b
        | n -> loop (n - 1L) (b, a + b)

    loop n (0L, 1L)

fib 50 |> printfn "%A"

let mapping = [ (3, "Fizz"); (5, "Buzz"); (7, "Bazz") ]

let fizzBuzz (initialMapping: (int * string) list) (n: int) =
    let rec loop mapping acc =
        match mapping with
        | [] -> if acc = "" then string n else acc
        | head :: tail ->
            let value = head |> fun (div, msg) -> if n % div = 0 then msg else ""
            loop tail (acc + value)

    loop initialMapping ""

[ 1..105 ] |> List.map (fizzBuzz mapping) |> List.iter (printfn "%s")

let fizzBuzzFold n =
    [ (3, "Fizz"); (5, "Buzz"); (7, "Bazz") ]
    |> List.fold (fun acc (div, msg) -> if n % div = 0 then acc + msg else acc) ""
    |> fun s -> if s = "" then string n else s

[ 1..105 ] |> List.iter (fizzBuzzFold >> printfn "%s")

let fizzBuzz2 n =
    [ (3, "Fizz"); (5, "Buzz") ]
    |> List.fold
        (fun acc (div, msg) ->
            match (if n % div = 0 then msg else "") with
            | "" -> acc
            | s -> if acc = string n then s else acc + s)
        (string n)

let rec quickSort input =
    match input with
    | [] -> []
    | head :: tail ->
        let smaller, larger = List.partition (fun n -> n <= head) tail
        List.concat [ quickSort smaller; [ head ]; quickSort larger ]

[ 5; 9; 5; 2; 7; 9; 1; 1; 3; 5 ] |> quickSort |> printfn "%A"
