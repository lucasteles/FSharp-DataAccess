module SeriesData

open Microsoft.EntityFrameworkCore
open Series.Data

let getContext =
    let optionsBuilder = new DbContextOptionsBuilder<SerieContext>();
    optionsBuilder
        .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=SeriesDB;Integrated Security=True;MultipleActiveResultSets=True")
        |> ignore

    new SerieContext(optionsBuilder.Options)

let getSerie  = SerieRepository.getSerie getContext
let getEpisodesOfSerie = SerieRepository.getEpisodesOfSerie getContext
let addSerie = SerieRepository.addSerie getContext
let addSerieAsync = SerieRepository.addSerieAsync getContext
let updateSerie = SerieRepository.updateSerie getContext
let deleteSerie = SerieRepository.deleteSerie getContext
let getSeriesWithAiredEpisodes = SerieRepository.getSeriesWithAiredEpisodes getContext

let disposeContext () =
    use c = getContext
    ()