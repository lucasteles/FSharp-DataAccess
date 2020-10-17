open System

open FluentMigrator.Runner
open Microsoft.Extensions.DependencyInjection

open Migrations
let conn = @"Server=localhost;Database=SeriesDB;User=sa;Password=Senha@123;"
let createServices() =
        (new ServiceCollection())
            .AddFluentMigratorCore()
            .ConfigureRunner(
                fun rb ->
                   rb.AddSqlServer()
                     .WithGlobalConnectionString(conn)
                     .ScanIn( (typedefof<EpisodesMigration>).Assembly)
                     .For.Migrations
                     |> ignore)
            .AddLogging(fun lb -> lb.AddFluentMigratorConsole() |> ignore)
            .BuildServiceProvider(false)
            :> IServiceProvider

let updateDatabase (serviceProvider: IServiceProvider) =
    let runner = serviceProvider.GetRequiredService<IMigrationRunner>()
    runner.MigrateUp() |> ignore

[<EntryPoint>]
let main argv =
    printfn "Series Migrations..."
    let serviceProvider = createServices()
    use scope = serviceProvider.CreateScope()
    updateDatabase scope.ServiceProvider
    0 // return an integer exit code
