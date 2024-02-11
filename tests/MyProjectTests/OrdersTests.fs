namespace MyProjectTests.OrderTests

open MyProject.Orders
open MyProject.Orders.Domain
open Xunit
open FsUnit

module ``add multiple items to an order`` =

    [<Fact>]
    let ``when product does not exist in empty order`` () =
        let myEmptyOrder = { Id = 1; Items = [] }
        let expected = { Id = 1; Items = [ { ProductId = 1; Quantity = 3 } ] }
        let actual = myEmptyOrder |> addItem { ProductId = 1; Quantity = 3 }
        actual |> should equal expected

    [<Fact>]
    let ``when product does not exist in non empty order`` () =
        let myOrder = { Id = 1; Items = [ { ProductId = 1; Quantity = 1 } ] }
        let expected = { Id = 1; Items = [ { ProductId = 1; Quantity = 1 }; { ProductId = 2; Quantity = 5 } ] }
        let actual = myOrder |> addItem { ProductId = 2; Quantity = 5 }
        actual |> should equal expected
        
    [<Fact>]
    let ``when product exists in non empty order`` () =
        let myOrder = { Id = 1; Items = [ { ProductId = 1; Quantity = 1 } ] }
        let expected = { Id = 1; Items = [ { ProductId = 1; Quantity = 4 } ] }
        let actual = myOrder |> addItem { ProductId = 1; Quantity = 3 }
        actual |> should equal expected

module ``Add multiple items to an order`` =
    
    [<Fact>]
    let ``when new products are added to an order`` () =
        let myEmptyOrder = { Id = 1; Items = [] }
        let expected = { Id = 1; Items = [ { ProductId = 1; Quantity = 1 }; { ProductId = 2; Quantity = 5 } ] }
        let actual = myEmptyOrder |> addItems [ { ProductId = 1; Quantity = 1 }; { ProductId = 2; Quantity = 5 }; ]
        actual |> should equal expected
        
    [<Fact>]
    let ``when new products and updated existing to order`` () =
        let myOrder = { Id = 1; Items = [ { ProductId = 1; Quantity = 1 } ] }
        let expected = { Id = 1; Items = [ { ProductId = 1; Quantity = 2 }; { ProductId = 2; Quantity = 5 }] }
        let actual = myOrder |> addItems [ { ProductId = 1; Quantity = 1 }; { ProductId = 2; Quantity = 5 } ]
        actual |> should equal expected

module ``Removing a product`` =
    
    [<Fact>]
    let ``when remove all items of existing product id`` () =
        let myOrder = { Id = 1; Items = [ { ProductId = 1; Quantity = 1 } ] }
        let expected = { Id = 1; Items = [] }
        let actual = myOrder |> removeProduct 1
        actual |> should equal expected
        
    [<Fact>]
    let ``should do nothing for non existent product id`` () =
        let myOrder = { Id = 2; Items = [ { ProductId = 1; Quantity = 1 } ] }
        let expected = { Id = 2; Items = [ { ProductId = 1; Quantity = 1 } ] }
        let actual = myOrder |> removeProduct 2
        actual |> should equal expected
