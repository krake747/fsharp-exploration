﻿open System.IO

type Tree<'T> =
    | Branch of 'T * Tree<'T> seq
    | Leaf of 'T

type Connection = { Start: string; Finish: string; Distance: int }

type Waypoint = { Location: string; Route: string list; TotalDistance: int }

// Load the data
let loadData path =
    path
    |> File.ReadLines
    |> Seq.skip 1
    |> fun rows -> [
        for row in rows do
            match row.Split(",") with
            | [| start; finish; distance |] ->
                { Start = start; Finish = finish; Distance = int distance }
                { Start = finish; Finish = start; Distance = int distance }
            | _ -> failwith "Row is badly formed"
    ]
    |> List.groupBy (_.Start)
    |> Map.ofList

// Find possible routes
let getUnvisited connections current =
    connections
    |> List.filter (fun cn -> current.Route |> List.exists (fun loc -> loc = cn.Finish) |> not)
    |> List.map (fun cn -> {
        Location = cn.Finish
        Route = cn.Start :: current.Route
        TotalDistance = cn.Distance + current.TotalDistance
    })

let rec treeToList tree =
    match tree with
    | Leaf x -> [ x ]
    | Branch(_, xs) -> List.collect treeToList (xs |> Seq.toList)

let findPossibleRoutes start finish (routeMap: Map<string, Connection list>) =
    let rec loop current =
        let nextRoutes = getUnvisited routeMap[current.Location] current

        if nextRoutes |> List.isEmpty |> not && current.Location <> finish then
            Branch(
                current,
                seq {
                    for next in nextRoutes do
                        loop next
                }
            )
        else
            Leaf current

    loop { Location = start; Route = []; TotalDistance = 0 }
    |> treeToList
    |> List.filter (fun wp -> wp.Location = finish)

let selectShortestRoute routes =
    routes
    |> List.minBy (_.TotalDistance)
    |> fun wp -> wp.Location :: wp.Route |> List.rev, wp.TotalDistance

let run start finish =
    Path.Combine(__SOURCE_DIRECTORY__, "resources", "data.csv")
    |> loadData
    |> findPossibleRoutes start finish
    |> selectShortestRoute
    |> printfn "%A"

let result = run "Cogburg" "Leverstorm"
