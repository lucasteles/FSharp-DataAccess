namespace Series.Data

open Prelude
open System.Linq
open Microsoft.EntityFrameworkCore


module SerieRepository =
    let getSerie (context: SerieContext) id =
        context
            .Series
            .Include(fun s -> s.Episodes)
            .FirstOrDefault (fun f -> f.Id = id)
            |> Option.mapIfNull

    let getEpisodesOfSerie (context: SerieContext) serieId =
        context
             .Episodes
             .Where(fun e -> e.SerieId = serieId )
             |> List.ofSeq

    let addSerieAsync (context: SerieContext) (entity: Serie) =
        async {
            context.Series.Add(entity) |> ignore
            context.SaveChangesAsync true |> Async.AwaitTask |> ignore
        }

    let addSerie (context: SerieContext) (entity: Serie) =
        context.Series.Add entity |> ignore
        context.SaveChanges() |> ignore

    let updateSerie (context: SerieContext) (entity: Serie) =
        let currentEntry = context.Series.Find(entity.Id)
        context.Entry(currentEntry).CurrentValues.SetValues(entity)
        context.SaveChanges |> ignore

    let deleteSerie (context: SerieContext) (entity: Serie) =
        context.Series.Remove entity |> ignore
        context.SaveChanges |> ignore

    let getSeriesWithAiredEpisodes (context: SerieContext) =
        query {
            for serie in context.Series do
                where (serie.Episodes.Any(fun e -> e.Status = EpisodeStatus.Aired))
                select serie
        }
