module BikeStore.Domain.Payments

type Undefined = exn

[<Struct>]
type CheckNumber = CheckNumber of int

type CardNumber = CardNumber of string
type CardType = Visa | MasterCard

type CreditCardInfo = {
    CardType: CardType
    CardNumber: CardNumber
}

type PaymentMethod =
    | Cash
    | Check of CheckNumber
    | Card of CreditCardInfo

type PaymentAmount = PaymentAmount of decimal
type Currency = EUR | USD

type PaymentError =
    | CardTypeNotRecognized
    | PaymentRejected
    | PaymentProviderOffline

type Payment = {
    Amount: PaymentAmount
    Currency: Currency
    Method: PaymentMethod
}


type UnpaidInvoice = PaymentAmount
type PaidInvoice = PaymentAmount

type PayInvoice = UnpaidInvoice -> Payment -> Result<PaidInvoice, PaymentError>

type ConvertPaymentCurrency = Payment -> Currency -> Payment

