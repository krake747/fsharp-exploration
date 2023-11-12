module BikeStore.OrderModule.PlaceOrder

open BikeStore.Common

type CustomerInfoValidator = UnvalidatedCustomerInfo -> CustomerInfo

let customerInfoValidator: CustomerInfoValidator =
    fun unvalidatedCustomerInfo ->
        let { FirstName = fn; LastName = ln; EmailAddress = email } =
            unvalidatedCustomerInfo
        {
            Name = { FirstName = fn; LastName = ln }
            EmailAddress =
                match EmailAddress.create email with
                | Ok e -> e
                | Error err -> failwith err
        }

type ValidateOrderForm = UnvalidatedOrder -> PricedOrder
