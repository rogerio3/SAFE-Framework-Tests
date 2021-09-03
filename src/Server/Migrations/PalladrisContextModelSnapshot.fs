﻿// <auto-generated />
namespace Migrations

open System
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Infrastructure
open Microsoft.EntityFrameworkCore.Metadata
open Microsoft.EntityFrameworkCore.Migrations
open Microsoft.EntityFrameworkCore.Storage.ValueConversion
open Npgsql.EntityFrameworkCore.PostgreSQL.Metadata

[<DbContext(typeof<DomainModels.PalladrisContext>)>]
type PalladrisContextModelSnapshot() =
    inherit ModelSnapshot()

    override this.BuildModel(modelBuilder: ModelBuilder) =
        modelBuilder

            .UseIdentityByDefaultColumns().HasAnnotation("Relational:MaxIdentifierLength", 63)
            .HasAnnotation("ProductVersion", "5.0.9")
            |> ignore

        modelBuilder.Entity("DomainModels+palladristransaction", (fun b ->

            b.Property<int>("Id")
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnType("integer")
                .UseIdentityByDefaultColumn() |> ignore
            b.Property<string>("Pairs")
                .IsRequired(true)
                .HasColumnType("text") |> ignore
            b.Property<double>("Price")
                .IsRequired(true)
                .HasColumnType("double precision") |> ignore
            b.Property<string>("Provider")
                .IsRequired(true)
                .HasColumnType("text") |> ignore
            b.Property<int>("Quantity")
                .IsRequired(true)
                .HasColumnType("integer") |> ignore
            b.Property<DateTime>("TransactionDate")
                .IsRequired(true)
                .HasColumnType("timestamp without time zone") |> ignore

            b.HasKey("Id") |> ignore

            b.ToTable("palladristransaction") |> ignore

        )) |> ignore

