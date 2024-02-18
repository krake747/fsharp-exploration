module BikeStore.OrderModule.Products

open BikeStore.Common
open BikeStore.Common.ResultExtensions
open Microsoft.FSharp.Core

// ---
// Bike
// ---

[<Struct>]
type BikeId = BikeId of int

type BikeBrand = BikeBrand of string
type BikeCategory = CategoryName of string

type BikeModel = {
    Name: string
    Year: int
    Brand: BikeBrand
    Category: BikeCategory
}

type Bike = { Id: BikeId; Model: BikeModel; ListPrice: Price }

// ---
// Clothing
// ---

[<Struct>]
type ClothingId = ClothingId of int

type ClothingBrand = ClothingBrand of string
type ClothingCategory = ClothingCategory of string

type ClothingSize =
    | Small = 1
    | Medium = 2
    | Large = 3

type ClothingModel = {
    Name: string
    Size: ClothingSize
    Brand: ClothingBrand
    Category: ClothingCategory
}

type Clothing = { Id: ClothingId; Model: ClothingModel; ListPrice: Price }

type ProductCode =
    | BikeCode of BikeId
    | ClothingCode of ClothingId

// ---
// Product Catalog
// ---

let getBikes () : Bike list = [
    {
        Id = BikeId 1
        Model = {
            Name = "Top Fuel"
            Year = 2018
            Brand = BikeBrand "Trek"
            Category = CategoryName "Cross Country"
        }
        ListPrice = Price.create 5100m |> failOnError
    }
    {
        Id = BikeId 2
        Model = {
            Name = "Grail CFR Di2"
            Year = 2023
            Brand = BikeBrand "Canyon"
            Category = CategoryName "Gravel"
        }
        ListPrice = Price.create 6600m |> failOnError
    }
    {
        Id = BikeId 3
        Model = {
            Name = "Revolt Advanced Pro 0"
            Year = 2022
            Brand = BikeBrand "Giant"
            Category = CategoryName "Gravel"
        }
        ListPrice = Price.create 4300m |> failOnError
    }
]

let getClothes () : Clothing list = [
    {
        Id = ClothingId 4
        Model = {
            Name = "Puro 3 FZ"
            Size = ClothingSize.Large
            Brand = ClothingBrand "Castelli"
            Category = ClothingCategory "Jerseys"
        }
        ListPrice = Price.create 180m |> failOnError
    }
    {
        Id = ClothingId 5
        Model = {
            Name = "Mille GTO Bib Shorts"
            Size = ClothingSize.Medium
            Brand = ClothingBrand "Assos"
            Category = ClothingCategory "Trousers"
        }
        ListPrice = Price.create 120m |> failOnError
    }
    {
        Id = ClothingId 6
        Model = {
            Name = "Aries Spherical Helmet"
            Size = ClothingSize.Medium
            Brand = ClothingBrand "POC"
            Category = ClothingCategory "Helmets"
        }
        ListPrice = Price.create 300m |> failOnError
    }
]

let mergeDistinct l1 l2 = l1 @ l2 |> List.distinct

let bikeIds = getBikes () |> List.map (fun b -> BikeCode(b.Id))
let clothingIds = getClothes () |> List.map (fun c -> ClothingCode(c.Id))

let getProductCatalog () : ProductCode list = mergeDistinct bikeIds clothingIds
