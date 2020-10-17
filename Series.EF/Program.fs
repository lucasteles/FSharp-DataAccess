open System
open SeriesData
open Series.Data
open System.Linq

let changeName (s: Serie) =
    let news = { s with Name = s.Name + "!" }
    updateSerie news

let log (x:string) = Console.WriteLine x

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
            SerieId = 0
        }]

    let newSerie = {
        Id = 0
        Name = "Super duper serie"
        Description = Some "Some description of this awesome serie"
        Episodes = episodes.ToList()
        Status = SerieStatus.New
    }

    let newSerie2 = {
        Id = 0
        Name = "Bad serie"
        Description = None
        Episodes = [].ToList()
        Status = SerieStatus.Ended
    }

    log "Add a serie with an episode"
    addSerie newSerie
    addSerie newSerie2

    log "Retrieving the newly added episode"
    let serie = getSerie newSerie.Id

    let serieN = getSerie -1

    log "Retrieving the episodes of a serie"
    let episodes = getEpisodesOfSerie newSerie.Id

    log "Changing the name of this serie"
    match serie with
    | Some s -> changeName s
    | _ -> ()

    log "Deleting the serie from the database"
    match serie with
    | Some s -> deleteSerie s
    | _ -> ()

    log "Get all series with aired episodes"
    getSeriesWithAiredEpisodes
    |> Seq.iter (fun s ->  printf "Serie: %s %d\n" s.Name s.Id)

    log "Add a serie with an episode... Synchronously"
    addSerieAsync newSerie |> Async.RunSynchronously |> ignore

    disposeContext ()

    log "Press any key to exit"
    Console.ReadKey() |> ignore
    0