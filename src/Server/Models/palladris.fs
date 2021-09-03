module DomainModels

open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore
open EntityFrameworkCore.FSharp.Extensions
open System


[<CLIMutable>]
type palladristransaction =
    {
        id : int
        [<Required>]
        pairs : string
        [<Required>]
        provider : string
        [<Required>]
        price : float
        [<Required>]
        quantity : int
        [<Required>]
        transactiondate : DateTime
    }

type PalladrisContext() =
    inherit DbContext()

    [<DefaultValue>] val mutable Palladristransaction : DbSet<palladristransaction>
    member this.palladristransaction with get() = this.palladristransaction and set v = this.palladristransaction <- v

    override _.OnModelCreating builder =
        builder.RegisterOptionTypes() // enables option values for all entities

    override __.OnConfiguring(options: DbContextOptionsBuilder) : unit =
        options.UseNpgsql(@"host=localhost;database=palladris;user id=postgres;password=docker;")
        |> ignore