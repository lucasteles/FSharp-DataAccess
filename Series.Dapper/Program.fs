open System
open Series.Data

let log  = printfn "%s"

[<EntryPoint>]
let main _ =
    let episodes =
        [{
            Id = 0
            SerieId = 0
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
    let id = SeriesRepository.addSerie newSerie |> Async.RunSynchronously

    log "Retrieving the newly added episode"
    let serie = SeriesRepository.getSerie id |> Async.RunSynchronously

    printfn "%A" serie   

    Console.ReadKey() |> ignore
    0