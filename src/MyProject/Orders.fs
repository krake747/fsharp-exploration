namespace MyProject.Orders


type Item = { ProductId: int; Quantity: int }

type Order = { Id: int; Items: Item list }

module Domain =
    // Add an item
    // 1 - Prepend new item to existing order items
    // 2 - Consolidate each product
    // 3 - Sort items in order by product id to make equality simpler
    // 4 - Update order with new list of items

    let recalculate items =
        items
        |> List.groupBy (_.ProductId)
        |> List.map (fun (id, items) -> {
            ProductId = id
            Quantity = items |> List.sumBy (_.Quantity)
        })
        |> List.sortBy (_.ProductId)

    let addItem item order =
        let items = item :: order.Items |> recalculate
        { order with Items = items }

    let addItems items order =
        let items = items @ order.Items |> recalculate
        { order with Items = items }

    // Remove an item
    let removeProduct productId order =
        let items =
            order.Items
            |> List.filter (fun x -> x.ProductId <> productId)
            |> List.sortBy _.ProductId

        { order with Items = items }

    // Reduce quantity of an item
    let reduceItem productId quantity order =
        let items =
            { ProductId = productId; Quantity = -quantity } :: order.Items
            |> recalculate
            |> List.filter (fun x -> x.Quantity > 0)

        { order with Items = items }

    // Clear all of the items
    let clearItems order = { order with Items = [] }
