namespace Series.Data

open FSharp.Data.Sql
open System.Linq
open System
open Db

module SeriesRepository =

    let addSerie (entity: Serie) =
        let newSerie = context.Dbo.Series.Create(
                                entity.Description,
                                entity.Name,
                                entity.Status.ToString())

        context.SubmitUpdates()
        for e in entity.Episodes do
            context.Dbo.Episodes.Create(
                e.Description,
                e.Name,
                e.Number,
                e.Season,
                newSerie.Id,
                e.Status.ToString()) |> ignore

        context.SubmitUpdates()
        newSerie

    let getSerie (idSerie: SerieId) =
        query {
            for serie in context.Dbo.Series do
            for ep in (!!) serie.``dbo.Episodes by Id`` do
                where (serie.Id = idSerie)
                select (serie, ep )
        }

