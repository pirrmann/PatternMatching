let input = (1, 0)

let result =
    match input with
    | 1, 1 -> 1
    | 2, _ -> 2
    | _, 3 -> 3
    | a, b when a > b -> 4
    | _ -> 5