namespace BikeStore.Domain

type Undefined = exn

// OrderTaking.Types.fs
module OrderTaking =
    // Value Objects
    // Product Code related
    type WidgetCode = WidgetCode of string
    type GizmoCode = GizmoCode of string

    type ProductCode =
        | Widget of WidgetCode
        | Gizmo of GizmoCode

    // Order Quantity related
    [<Struct>]
    type UnitQuantity = UnitQuantity of int

    [<Struct>]
    type KilogramQuantity = KilogramQuantity of decimal

    type OrderQuantity =
        | Unit of UnitQuantity
        | Kilos of KilogramQuantity

    // Entities
    type OrderId = Undefined
    type OrderLineId = Undefined
    type CustomerId = Undefined

    type CustomerInfo = Undefined
    type ShippingAddress = Undefined
    type BillingAddress = Undefined
    type Price = Undefined
    type BillingAmount = Undefined

    type Order = {
        Id: OrderId
        CustomerId: CustomerId
        ShippingAddress: ShippingAddress
        BillingAddress: BillingAddress
        OrderLines: OrderLine list
        AmountToBill: BillingAmount
    }

    and OrderLine = {
        Id: OrderLineId
        OrderId: OrderId
        ProductCode: ProductCode
        OrderQuantity: OrderQuantity
        Price: Price
    }

    // Workflow
    // Input
    type UnvalidatedCustomerInfo = {
        FirstName: string
        LastName: string
        EmailAddress: string
    }

    type UnvalidatedAddress = {
        AddressLine1: string
        AddressLine2: string
        AddressLine3: string
        AddressLine4: string
        City: string
        ZipCode: string
    }

    type UnvalidatedOrderLine = {
        OrderLineId: string
        ProductCode: string
        Quantity: decimal
    }

    type UnvalidatedOrder = {
        OrderId: string
        CustomerInfo: UnvalidatedCustomerInfo
        ShippingAddress: UnvalidatedAddress
        BillingAddress: UnvalidatedAddress
        Lines: UnvalidatedOrderLine list
    }

    // Events
    type OrderAcknowledgmentSent = Undefined
    type OrderPlaced = Undefined
    type BillableOrderPlaced = Undefined

    type PlaceOrderEvent =
        | OrderPlaced of OrderPlaced
        | BillableOrderPlaced of BillableOrderPlaced
        | AcknowledgmentSent of OrderAcknowledgmentSent

    // Errors
    type ValidationError = ValidationError of string
    type PricingError = PricingError of string

    type PlaceOrderError =
        | Validation of ValidationError
        | Pricing of PricingError
