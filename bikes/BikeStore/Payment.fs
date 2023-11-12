module BikeStore.Payment

type CheckNumber = CheckNumber of int
type CardNumber = CardNumber of string

type CardType =
    | Visa
    | Mastercard

type CreditCardInfo = { CardType: CardType; CardNumber: CardNumber }

type PaymentMethod =
    | Cash
    | Check of CheckNumber
    | Card of CreditCardInfo

type PaymentAmount = PaymentAmount of decimal

type Currency =
    | EUR
    | USD

type Payment = {
    Amount: PaymentAmount
    Currency: Currency
    Method: PaymentMethod
}

type UnpaidInvoice = Undefined
type PaidInvoice = Undefined

type PayInvoice = UnpaidInvoice -> Payment -> PaidInvoice

type ConvertPaymentCurrency = Payment -> Currency -> Payment
