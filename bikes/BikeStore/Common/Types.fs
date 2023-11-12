namespace BikeStore.Common

open System
open System.Text.RegularExpressions

// ---
// Simple and Constrained Types
// ---

/// An email address
type EmailAddress = private EmailAddress of string

/// A zip code
type Country = Country of string

/// A zip code
type City = City of string

/// A zip code
type ZipCode = private ZipCode of string

/// Constrained to be a integer between 1 and 100
[<Struct>]
type UnitQuantity = private UnitQuantity of int

/// Constrained to be a integer between 1 and 10
[<Struct>]
type OrderQuantity = Unit of UnitQuantity

/// Constrained to be a decimal between 0.0 and 50000.00
[<Struct>]
type Price = private Price of decimal

/// Constrained to be a decimal between 0.0 and 500000.00
[<Struct>]
type BillingAmount = private BillingAmount of decimal

// ---
// Smart Factory Constructors
// ---
module EmailAddress =
    let create email =
        if String.IsNullOrEmpty(email) then
            Error "Email cannot be null or empty"
        elif not (Regex.IsMatch(email, "@")) then
            Error "Email must match the pattern '@'"
        else
            Ok(EmailAddress email)

    let value (EmailAddress email) = email

module UnitQuantity =
    let create qty =
        if qty < 1 then Error "UnitQuantity cannot be negative"
        elif qty > 10 then Error "UnitQuantity cannot be more than 10"
        else Ok(UnitQuantity qty)

    let value (UnitQuantity qty) = qty

module Price =
    let create price =
        if price < 0m then Error "Price must not be less than 0"
        elif price > 50000m then Error "Price must not be greater thatn 50'000"
        else Ok(Price price)
        
    let value (Price price) = price
