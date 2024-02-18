open System

// Equality
[<AllowNullLiteral>]
type GpsCoordinate(latitude: float, longitude: float) =
    let equals (other: GpsCoordinate) =
        if isNull other then
            false
        else
            latitude = other.Latitude && longitude = other.Longitude
    
    member _.Latitude = latitude
    member _.Longitude = longitude
    
    override this.GetHashCode() =
        hash (this.Latitude, this.Longitude)
    
    override _.Equals(obj) =
        match obj with
        | :? GpsCoordinate as other -> equals other
        | _ -> false
        
    interface IEquatable<GpsCoordinate> with
        member _.Equals(other: GpsCoordinate) =
            equals other
          
    static member op_Equality(this: GpsCoordinate, other: GpsCoordinate) =
        this.Equals(other)
            
let c1 = GpsCoordinate(25.0, 11.98)
let c2 = GpsCoordinate(25.0, 11.98)

c1 = c2 |> printfn "%A"


