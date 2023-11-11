namespace BikeStore.Domain

open OrderTaking

module OrderTaking =
    type PlaceOrder =
        UnvalidatedOrder -> Result<PlaceOrderEvent, PlaceOrderError>    

