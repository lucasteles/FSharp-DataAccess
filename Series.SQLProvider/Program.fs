open System
open Series.Data
open System.Linq
open FSharp.Data.Sql
open Series.Data

let log  = printfn "%s"

[<EntryPoint>]
let main _ =
    let episodes =
        [{
            Id = 0
            Number = 1
            Season = 1
            Status = EpisodeStatus.Scheduled
            Name = "The start of the first beginning intro"
            Description = "This episode explains everything"
        }]

    let newSerie = {
        Id = 0
        Name = "Super duper serie"
        Description = "Some description of this awesome serie"
        Episodes = episodes
        Status = SerieStatus.New
    }

    log "Add a serie with an episode"
    let otherSerie = SeriesRepository.addSerie newSerie

    //log "Retrieving the newly added episode"
    //let serie = SeriesRepository.getSerie otherSerie.Id
    //match serie with
    //| Some s -> printfn "%s" s.Name
    //| _ -> ()

    //log "Retrieving the episodes of a serie"
    //let episodes = getEpisodesOfSerie newSerie.Id

    //log "Changing the name of this serie"
    //match serie with
    //| Some s -> changeName s
    //| _ -> ()

    //log "Deleting the serie from the database"
    //match serie with
    //| Some s -> deleteSerie s
    //| _ -> ()

    //log "Get all series with aired episodes"
    //getSeriesWithAiredEpisodes
    //|> Seq.iter (fun s ->  printf "Serie: %s %d\n" s.Name s.Id)

    //log "Add a serie with an episode... Synchronously"
    //addSerieAsync newSerie |> Async.RunSynchronously |> ignore

    log "Press any key to exit"
    Console.ReadKey() |> ignore
    0