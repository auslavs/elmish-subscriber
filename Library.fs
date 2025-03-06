module test_elmish

open Elmish
open Feliz
open Feliz.UseElmish
open System
open Fable.Core.JS

type Model = {
    Count : int
}

type Msg =
    | Refresh

let init initialValue =
    {
        Count = initialValue
    },
    Cmd.ofMsg Refresh

let update msg (model:Model) =
    match msg with
    | Refresh ->
        printfn "refreshing log history"
        { model with Count = model.Count + 1}, Cmd.none

let subscribeToTimer dispatch =
    let subscriptionId =
        setInterval
            (fun _ ->
                printfn "Subscriber: sending refresh"
                dispatch Refresh)
            5000
    { new IDisposable with member _.Dispose() = clearTimeout subscriptionId }

let makeProgram() =
    Program.mkProgram init update (fun _ _ -> ())
    |> Program.withSubscription (fun _model ->
        [ ["timer"], subscribeToTimer ])

[<ReactComponent>]
let LogHistory
    (props:
        {|
            initialValue : int
        |})
    =

    let model, dispatch =
        React.useElmish (makeProgram, props.initialValue, [||])

    Html.div [
        prop.children [
            Html.button [
                prop.onClick (fun _ -> dispatch Refresh)
                prop.text "Refresh"
            ]

            Html.text (string model.Count)
        ]
    ]