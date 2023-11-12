module BikeStore.OrderModule.PlaceOrder

open BikeStore.Common

type CustomerInfoValidator = UnvalidatedCustomerInfo -> CustomerInfo

let customerInfoValidator: CustomerInfoValidator =
    fun (unvalidatedCustomerInfo: UnvalidatedCustomerInfo) ->    
    {
        Name = {
            FirstName = unvalidatedCustomerInfo.FirstName 
            LastName = unvalidatedCustomerInfo.LastName 
        }
        EmailAddress =
            match EmailAddress.create unvalidatedCustomerInfo.EmailAddress with
            | Ok e -> e
            | Error err -> failwith err
    }

type ValidateOrderForm = UnvalidatedOrder -> PricedOrder
