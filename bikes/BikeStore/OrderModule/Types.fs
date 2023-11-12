namespace BikeStore.OrderModule

open BikeStore.Common
open BikeStore.OrderModule.Products

// ---
// Inputs
// ---

/// A unique OrderId
[<Struct>]
type OrderId = OrderId of int

/// A unique OrderLineId
[<Struct>]
type OrderLineId = OrderLineId of int

type UnvalidatedCustomerInfo = {
    FirstName: string
    LastName: string
    EmailAddress: string
}

type UnvalidatedAddress = {
    AddressLine1: string
    AddressLine2: string
    City: string
    ZipCode: string
    Country: string
}

type UnvalidatedOrderLine = {
    Id: OrderLineId
    ProductCode: ProductCode
    Quantity: OrderQuantity
}

type UnvalidatedOrder = {
    Id: OrderId
    CustomerInfo: UnvalidatedCustomerInfo
    ShippingAddress: UnvalidatedAddress
    BillingAddress: UnvalidatedAddress
    Lines: UnvalidatedOrderLine list
}

// ---
// Outputs
// ---

type PricedOrderLine = {
    OrderLineId: OrderLineId
    ProductCode: ProductCode
    OrderQuantity: OrderQuantity
    LinePrice: Price
}

type PricedOrder = {
    Id: OrderId
    CustomerInfo: CustomerInfo
    ShippingAddress: Address
    BillingAddress: Address
    BillingAmount: Price
    Lines: PricedOrderLine list
}

// ---
// Output Events
// ---

type OrderAcknowledgmentSent = { OrderId: OrderId; EmailAddress: EmailAddress }

type OrderPlaced = PricedOrder

type BillableOrderPlaced = {
    OrderId: OrderId
    BillingAddress: Address
    BillingAmount: BillingAmount
}

type PlaceOrderEvent =
    | OrderPlaced of OrderPlaced
    | BillableOrderPlaced of BillableOrderPlaced
    | AcknowledgmentSent of OrderAcknowledgmentSent
