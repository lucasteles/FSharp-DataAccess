module SeriesData

open Microsoft.EntityFrameworkCore
open Series.Data

let private getContext () =
    let optionsBuilder = new DbContextOptionsBuilder<SerieContext>();
    optionsBuilder
        .UseSqlServer(@"Server=localhost;Database=SeriesDB;User=sa;Password=Senha@123;MultipleActiveResultSets=True")
        |> ignore
    new SerieContext(optionsBuilder.Options)

let context = getContext()

let getSerie  = SerieRepository.getSerie context
let getEpisodesOfSerie = SerieRepository.getEpisodesOfSerie context
let addSerie = SerieRepository.addSerie context
let addSerieAsync = SerieRepository.addSerieAsync context
let updateSerie = SerieRepository.updateSerie context
let deleteSerie = SerieRepository.deleteSerie context
let getSeriesWithAiredEpisodes = SerieRepository.getSeriesWithAiredEpisodes context
let disposeContext () =
    use c = context
    ()