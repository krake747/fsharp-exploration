namespace Learning.Domain

open System

type MilesYards = MilesYards of wholeMiles: int * yards: int

module MilesYards =

    let fromMilesPointYards (milesPointYards: float) : MilesYards =
        if milesPointYards < 0.0 then
            raise <| ArgumentOutOfRangeException(nameof milesPointYards, "Must be > 0.0")

        let wholeMiles = milesPointYards |> floor |> int
        let fraction = milesPointYards - float wholeMiles

        if fraction > 0.1759 then
            raise
            <| ArgumentOutOfRangeException(nameof milesPointYards, "Fractional part must be <= 0.1759")

        let yards = fraction * 10000. |> round |> int

        MilesYards(wholeMiles, yards)

    let toDecimalMiles (MilesYards(wholeMiles, yards)) : float =
        (float wholeMiles) + ((float yards) / 1760.)


type MilesChains = MilesChains of wholeMiles: int * chains: int

module MilesChains =

    let fromMilesChains (wholeMiles: int, chains: int) =
        if wholeMiles < 0 then
            raise <| ArgumentOutOfRangeException(nameof wholeMiles, "Must be >= 0")

        if chains < 0 || chains >= 80 then
            raise <| ArgumentOutOfRangeException(nameof chains, "Must be >= 0 and < 80")

        MilesChains(wholeMiles, chains)

    let toDecimalMiles (MilesChains(wholeMiles, chains)) : float = float wholeMiles + (float chains / 80.)

module Billing =

    type BillingDetails = {
        Name: string
        Billing: string
        Delivery: string option
    }
    
    let tryLastLine (address : string) =
        let parts = address.Split('\n', StringSplitOptions.RemoveEmptyEntries)
        match parts with
        | [||] -> None
        | parts -> parts |> Array.last |> Some

    let tryPostalCode (codeString: string) =
        match Int32.TryParse codeString with
        | true, i -> i |> Some
        | false, _ -> None
        
    let postalCodeHub (code : int) =
        if code = 62291 then "Hub 1" else "Hub 2"    

    let tryHub (details: BillingDetails) =
        details.Delivery
        |> Option.bind tryLastLine
        |> Option.bind tryPostalCode
        |> Option.map postalCodeHub
