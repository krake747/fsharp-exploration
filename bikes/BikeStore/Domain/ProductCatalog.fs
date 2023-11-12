namespace BikeStore.Domain

module ProductCatalog =
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

    type Bike = { Id: BikeId; Model: BikeModel; ListPrice: decimal }
