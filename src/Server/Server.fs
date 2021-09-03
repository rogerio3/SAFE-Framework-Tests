module Server.API

// open System
// open System.Linq
// open System.Threading.Tasks
open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Saturn

open Shared
open DomainModels
open Npgsql.FSharp

let transactions = ResizeArray<palladristransaction>()
let connectionString = "host=localhost;database=palladris;user id=postgres;password=docker;"

let getTransactions (connectionString: string): palladristransaction list =
    connectionString
    |> Sql.connect
    |> Sql.query "SELECT * FROM palladristransaction"
    |> Sql.execute (fun read ->
    {
        id = read.int "id"
        pairs = read.text "pairs"
        provider = read.text "provider"
        price = read.double "price"
        quantity = read.int "quantity"
        transactiondate = read.dateTime "transactiondate"
    })


let AddTransaction connectionString (transactionData: palladristransaction) =
    try
        printfn "%A" connectionString
        connectionString
        |> Sql.connect
        |> Sql.query "INSERT INTO palladristransaction (pairs, provider, price, quantity, transactionDate) VALUES (@pairs, @provider, @price, @quantity, @transactiondate)"
        |> Sql.parameters [
            "pairs", Sql.text transactionData.pairs;
            "provider", Sql.text transactionData.provider;
            "price", Sql.double transactionData.price;
            "quantity", Sql.int transactionData.quantity;
            "transactiondate", Sql.timestamp transactionData.transactiondate;
            ]
        |> Sql.executeNonQuery
        |> Ok
    with
    | e -> Error "Database error occurred while saving transaction"

module Route =
    let builder typeName methodName =
        sprintf "/api/%s/%s" typeName methodName


type IApi =
    {
      getTransactions: unit -> Async<palladristransaction list>
      addTransaction: palladristransaction -> Async<int>
    }
let Api =
    {
        getTransactions = fun () -> async { return getTransactions(connectionString) }
        addTransaction =
          fun palladristransaction ->
              async {
                  match AddTransaction connectionString palladristransaction with
                  | Ok qt -> return qt
                  | Error e -> return failwith e
              }
    }

let webApp =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.fromValue Api
    |> Remoting.buildHttpHandler

let app =
    application {
        url "http://0.0.0.0:8085"
        use_router webApp
        memory_cache
        use_static "public"
        use_gzip
    }

run app
