namespace Series.Data

open FSharp.Data.Sql
open System.Linq
open System
open Db

module private Mapping = 
    let toEpisode (ep: EspisodesEntity) = 
        { Id = ep.Id
          Number = ep.Number
          Season = ep.Season
          Name = ep.Name
          Description = ep.Description
          Status = EpisodeStatus.Parse(ep.Status) }

    let toSerie (serie: SeriesEntity) = 
        { Id = serie.Id
          Name = serie.Name
          Description = serie.Description
          Episodes = serie.``dbo.Episodes by Id`` |> Seq.map toEpisode |> Seq.toList
          Status = SerieStatus.Parse(serie.Status) }


module SeriesRepository =
    let addSerie (entity: Serie): Serie =
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
        Mapping.toSerie newSerie

    let getSerie (idSerie: SerieId) =
        let serie = query {
            for serie in context.Dbo.Series do
            //for ep in (!!) serie.``dbo.Episodes by Id`` do
                where (serie.Id = idSerie)
                select serie
                head
        }
        Mapping.toSerie serie
