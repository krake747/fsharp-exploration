open BikeStore.Common
open BikeStore.OrderModule
open BikeStore.OrderModule.Products

printfn "Welcome to the BikeStore"

let failOnError result =
    match result with
    | Ok success -> success 
    | Error error -> failwithf $"{error}" 

let productCatalog = getProductCatalog()

let newCustomerInfo: UnvalidatedCustomerInfo = {
    FirstName =  "Kevin"
    LastName = "Kraemer"
    EmailAddress = "kevin.kraemer@email.com"
}

let newAddress: UnvalidatedAddress = {
    AddressLine1 = "32, rue Du Cure"
    AddressLine2 = ""
    City = "Luxembourg"
    ZipCode = "1368"
    Country = "Luxembourg"
}

let newOrderForm: UnvalidatedOrder = {
    Id = OrderId 1
    CustomerInfo = newCustomerInfo
    ShippingAddress = newAddress
    BillingAddress = newAddress
    Lines = [
        {
            Id = OrderLineId 1
            ProductCode = productCatalog[0]
            Quantity = OrderQuantity.create 1 |> failOnError
        }
    ]
}

printfn $"{newOrderForm}"
