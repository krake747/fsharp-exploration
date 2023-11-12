namespace BikeStore.Common

type PersonalName = {
    FirstName: string
    LastName: string
}

type CustomerInfo = {
    Name: PersonalName
    EmailAddress: EmailAddress
}

type Address = {
    AddressLine1: string
    AddressLine2: string option
    City: string
    ZipCode: string
    Country: string
}

