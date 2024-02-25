module GiraffeApi.TodoStore

open System
open System.Collections.Concurrent

type TodoId = Guid

type NewTodo = { Description: string }

type Todo = {
    Id: TodoId
    Description: string
    Created: DateTime
    IsCompleted: bool
}

type TodoStore() =
    let data = ConcurrentDictionary<TodoId, Todo>()

    let get id =
        let success, value = data.TryGetValue(id)
        if success then Some value else None

    member _.Create todo = data.TryAdd(todo.Id, todo)

    member _.Update id todo =
        data.TryUpdate(id, todo, data[id])

    member _.DeleteById id = data.TryRemove(id)

    member _.GetById id = get id

    member _.GetAll () = data.Values |> Seq.toArray
