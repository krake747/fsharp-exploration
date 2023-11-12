namespace BikeStore.Common

open System
open System.Text.RegularExpressions

// ---
// Simple and Constrained Types
// ---

/// An email address
type EmailAddress = private EmailAddress of string


/// Constrained to be a integer between 1 and 100
type UnitQuantity = private UnitQuantity of int

/// A Quantity is a Unit
type OrderQuantity =
    | Unit of UnitQuantity
    
/// A unique OrderId
[<Struct>]
type OrderId = OrderId of int

/// A unique OrderLineId
[<Struct>]
type OrderLineId = OrderLineId of int

// ---
// Smart Constructors
// ---
module EmailAddress =
    let create email =
        if String.IsNullOrEmpty(email) then Error "Email cannot be null or empty"
        elif not (Regex.IsMatch(email, "@")) then Error "Email must match the pattern '@'"
        else Ok (EmailAddress email)
        
    let value (EmailAddress email) = email


module UnitQuantity =
    let create qty =
        if qty < 1 then Error "UnitQuantity cannot be negative"
        elif qty > 100 then Error "UnitQuantity cannot be more than 100"
        else Ok (UnitQuantity qty)
        
    let value (UnitQuantity qty) = qty
        

