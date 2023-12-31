﻿open BikeStore.Common
open BikeStore.Common.ResultExtensions
open BikeStore.OrderModule
open BikeStore.OrderModule.Products
open BikeStore.OrderModule.PlaceOrder

printfn "Welcome to the BikeStore"

let productCatalog = getProductCatalog ()

let newCustomerInfo: UnvalidatedCustomerInfo = {
    FirstName = "Kevin"
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

let newOrderLine1: UnvalidatedOrderLine = {
    Id = OrderLineId 1
    ProductCode = productCatalog[0]
    Quantity = OrderQuantity.create 1 |> failOnError
}

let newOrderLine2: UnvalidatedOrderLine = {
    Id = OrderLineId 2
    ProductCode = productCatalog[5]
    Quantity = OrderQuantity.create 2 |> failOnError
}

let newOrderForm: UnvalidatedOrder = {
    Id = OrderId 1
    CustomerInfo = newCustomerInfo
    ShippingAddress = newAddress
    BillingAddress = newAddress
    Lines = [ newOrderLine1; newOrderLine2 ]
}

printfn $"{newOrderForm}"

let validCustomerInfo = customerInfoValidator newCustomerInfo

printfn $"Valid CustomerInfo {validCustomerInfo}"
