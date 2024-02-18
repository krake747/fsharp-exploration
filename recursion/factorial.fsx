// two cases:
// a base case, in this example where n equals 1, at which point the recursion ends.
// a general case of greater than 1 which is recursive.
let rec fact n =
    match n with
    | 1 -> 1
    | _ -> n * fact (n - 1)
