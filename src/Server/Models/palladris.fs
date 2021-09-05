module DomainModels

open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore
open EntityFrameworkCore.FSharp.Extensions
open System


[<CLIMutable>]
type Palladristransaction =
    {
        Id : int
        [<Required>]
        Pairs : string
        [<Required>]
        Provider : string
        [<Required>]
        Price : float
        [<Required>]
        Quantity : int
        [<Required>]
        TransactionDate : DateTime
    }

type PalladrisContext() =
    inherit DbContext()

    [<DefaultValue>] val mutable Palladristransaction : DbSet<Palladristransaction>
    member this.PalladrisTransaction with get() = this.Palladristransaction and set v = this.Palladristransaction <- v

    override _.OnModelCreating builder =
        builder.RegisterOptionTypes() // enables option values for all entities

    override __.OnConfiguring(options: DbContextOptionsBuilder) : unit =
        options.UseNpgsql(@"host=localhost;database=teste1;user id=postgres;password=docker;")
        |> ignore