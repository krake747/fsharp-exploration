#r "../src/MyProject/bin/Debug/net8.0/MyProject.dll"

open MyProject.Orders

let order = { Id = 1; Items = [ { ProductId = 1; Quantity = 1 } ] }
let newItemExistingProduct = { ProductId = 1; Quantity = 1 }
let newItemNewProduct = { ProductId = 2; Quantity = 2 }

let t1 =
    Domain.addItem newItemNewProduct order = {
                                                 Id = 1
                                                 Items = [
                                                     { ProductId = 1; Quantity = 1 }
                                                     { ProductId = 2; Quantity = 2 }
                                                 ]
                                             }

let t2 =
    Domain.addItem newItemExistingProduct order = { Id = 1; Items = [ { ProductId = 1; Quantity = 2 } ] }
