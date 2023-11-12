open BikeStore.Common
open BikeStore.OrderModule.ProductCatalog

printfn "Welcome to the BikeStore"

let setPrice price =
    match Price.create price with
    | Ok p -> p
    | Error e -> failwith e

let productCatalog = [
    {
        Id = BikeId 1
        Model = {
            Name = "Top Fuel"
            Year = 2018
            Brand = BikeBrand "Trek"
            Category = CategoryName "Cross Country"
        }
        ListPrice = setPrice 5000m
    }
    {
        Id = BikeId 2
        Model = {
            Name = "Grail CFR Di2"
            Year = 2023
            Brand = BikeBrand "Canyon"
            Category = CategoryName "Gravel"
        }
        ListPrice = setPrice 7000m
    }
    {
        Id = BikeId 3
        Model = {
            Name = "Revolt Advanced Pro 0"
            Year = 2022
            Brand = BikeBrand "Giant"
            Category = CategoryName "Gravel"
        }
        ListPrice = setPrice 4300m
    }
    {
        Id = BikeId 4
        Model = {
            Name = "Allez"
            Year = 2023
            Brand = BikeBrand "Specialized"
            Category = CategoryName "Road"
        }
        ListPrice = setPrice 2400m
    }
]

let krake = { FirstName = "Kevin"; LastName = "Kraemer" }

let krakeAddress = {
    AddressLine1 = "13, rue des Bikes"
    AddressLine2 = None
    City = "Hesperange"
    ZipCode = "7546"
    Country = "Luxembourg"
}
