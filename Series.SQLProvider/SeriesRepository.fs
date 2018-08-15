namespace Series.Data

open FSharp.Data.Sql
open System.Linq
open System

module SeriesRepository =

    let private context = Db.Sql.GetDataContext()

    let addSerie (entity: Serie) =
        let newSerie = context.Dbo.Series.Create(
                                entity.Description,
                                entity.Name,
                                entity.Status.ToString())

        context.SubmitUpdates()
        entity.Episodes
        |> Seq.map (
            fun e ->
                context.Dbo.Episodes.Create(
                            e.Description,
                            e.Name,
                            e.Number,
                            e.Season,
                            newSerie.Id,
                            e.Status.ToString()))
                            |> ignore

        context.SubmitUpdates()
        newSerie



    let getSerie (idSerie: int) =
        query {
            for serie in context.Dbo.Series do
            for ep in (!!) serie.``dbo.Episodes by Id`` do
                where (serie.Id = idSerie)
                select (serie, ep )
        }
        |> Seq.map (fun (s,e) ->
                      let serie = {
                        Id = s.Id
                        Name = s.Name
                        Description = s.Description
                        Status = Enum.Parse(typedefof<SerieStatus>, s.Status) :?> SerieStatus
                        Episodes = List.Empty
                      }

                      let episode =
                              if e.Id = 0 then None else Some {
                                    Id = e.Id
                                    Number = e.Number
                                    Season = e.Season
                                    Name = e.Name
                                    Description = e.Description
                                    Status = Enum.Parse(typedefof<EpisodeStatus>, e.Status) :?> EpisodeStatus
                                    Serie = Some serie
                                   }

                      (serie, episode)
                  )
        |> Seq.groupBy fst
        |> Seq.map (fun (s,es) -> (s, es  |> Seq.map snd |> Seq.choose id))
        |> Seq.map (fun (s,es) -> {s with Episodes = es |> Seq.toList})
        |> Seq.tryHead

