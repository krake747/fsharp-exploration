module Car

type DriveResult = {
    GasLeft : float
    IsOutOfGas : bool
}

let drive distance gas =
    let output = 
        if distance > 50 then gas / 2.0
        elif distance > 25 then gas - 10.0
        elif distance > 0 then gas - 1.0
        else gas
    {
        GasLeft = output
        IsOutOfGas = output <= 0.0 
    }