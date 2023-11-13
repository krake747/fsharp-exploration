module BikeStore.OrderModule.PlaceOrder

open BikeStore.Common
open BikeStore.Common.ResultExtensions

type CustomerInfoValidator = UnvalidatedCustomerInfo -> CustomerInfo

let customerInfoValidator: CustomerInfoValidator =
    fun unvalidatedCustomerInfo ->
        let { FirstName = fn; LastName = ln; EmailAddress = email } =
            unvalidatedCustomerInfo

        {
            Name = { FirstName = fn; LastName = ln }
            EmailAddress = EmailAddress.create email |> failOnError
        }

type ValidateOrderForm = UnvalidatedOrder -> PricedOrder
