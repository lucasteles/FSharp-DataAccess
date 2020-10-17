namespace Series.Data

open System
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Storage.ValueConversion
open Prelude

type SerieContext =
    inherit DbContext
    new() = { inherit DbContext() }
    new(options: DbContextOptions<SerieContext>) = { inherit DbContext(options) }

    override _.OnModelCreating modelBuilder =

        modelBuilder.Entity<Episode>()
            .Property(fun e -> e.Status)
            .HasConversion(EnumToStringConverter<EpisodeStatus>())
            |> ignore

        modelBuilder
            .Entity<Serie>()
            .Property(fun e -> e.Status)
            .HasConversion(EnumToStringConverter<SerieStatus>())
            |> ignore

    [<DefaultValue>]
    val mutable series: DbSet<Serie>
    member x.Series
        with get() = x.series
        and set v = x.series <- v

    [<DefaultValue>]
    val mutable episodes:DbSet<Episode>
    member x.Episodes
        with get() = x.episodes
        and set v = x.episodes <- v