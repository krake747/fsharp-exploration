namespace BikeStore.Domain

open BikeStore.Common
open ProductCatalog

type Undefined = exn 

module OrderTaking = 
    type ProductCode =
        | Bike of BikeId
       
    // ---
    // Place Order workflow
    // ---    
    
    // unvalidated state
    type UnvalidatedOrder = {
        Id: OrderId
        UnvalidatedCustomerInfo: string
        UnvalidatedShippingAddress: string
        UnvalidatedBillingAddress: string
        UnvalidatedOrderLine: UnvalidatedOrderLine list
    }
    and UnvalidatedOrderLine = {
        Id : OrderLineId 
        ProductCode: ProductCode
        OrderQuantity: OrderQuantity
        Price: decimal
    }

    // validated state
    type ValidatedOrder= {
        Id: OrderId
        ValidatedCustomerInfo: CustomerInfo
        ValidatedShippingAddress: Address
        ValidatedBillingAddress: Address
        ValidatedOrderLine: Undefined list
    }
    
    // priced state
    type Order = {
        Id: OrderId
        CustomerInfo: CustomerInfo
        ShippingAddress: Address
        BillingAddress: Address
        OrderLine: OrderLine list
        BillingAmount: decimal
    }
    and OrderLine = {
        Id : OrderLineId
        OrderId: OrderId
        ProductCode: ProductCode
        OrderQuantity: OrderQuantity
        Price: decimal
    }
    
    // side effect
    type Acknowledgment =
        | EmailSent of Undefined
        | LetterSent of Undefined
        
    type PlacedOrderAcknowledgment = {
        PlacedOrder: Order
        Acknowledgment: Acknowledgment
    }

    // final state
    type PlaceOrderEvents = {
        AcknowledgmentSent : PlacedOrderAcknowledgment
        OrderPlaced : Order
    }

    // ----------------------
    // Workflows
    // ----------------------
    type ValidateOrder = UnvalidatedOrder -> ValidatedOrder
    type PlaceOrder = ValidateOrder -> PlaceOrderEvents
