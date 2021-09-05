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

let transactions = ResizeArray<Palladristransaction>()
let connectionString = "host=localhost;database=teste1;user id=postgres;password=docker;"

let GetTransactions (connectionString: string): Palladristransaction list =
    connectionString
    |> Sql.connect
    |> Sql.query "SELECT * FROM \"PalladrisTransaction\""
    |> Sql.execute (fun read ->
    {
        Id = read.int "Id"
        Pairs = read.text "Pairs"
        Provider = read.text "Provider"
        Price = read.double "Price"
        Quantity = read.int "Quantity"
        TransactionDate = read.dateTime "TransactionDate"
    })


let AddTransaction connectionString (transactionData: Palladristransaction) =
    try
        connectionString
        |> Sql.connect
        |> Sql.query "INSERT INTO \"PalladrisTransaction\" (\"Pairs\", \"Provider\", \"Price\", \"Quantity\", \"TransactionDate\") VALUES (@Pairs, @Provider, @Price, @Quantity, @TransactionDate)"
        |> Sql.parameters [
            "Pairs", Sql.text transactionData.Pairs;
            "Provider", Sql.text transactionData.Provider;
            "Price", Sql.double transactionData.Price;
            "Quantity", Sql.int transactionData.Quantity;
            "TransactionDate", Sql.timestamp transactionData.TransactionDate;
            ]
        |> Sql.executeNonQuery
        |> Ok
    with
    | e -> Error e.Message

module Route =
    let builder typeName methodName =
        sprintf "/api/%s/%s" typeName methodName


type IApi =
    {
      GetTransactions: unit -> Async<Palladristransaction list>
      AddTransaction: Palladristransaction -> Async<int>
    }
let Api =
    {
        GetTransactions = fun () -> async { return GetTransactions(connectionString) }
        AddTransaction =
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
