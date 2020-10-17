namespace Series.Data

open FSharp.Data.Sql

module Db =
    FSharp.Data.Sql.Common.QueryEvents.SqlQueryEvent |> Event.add (printfn "Executing SQL: %O")

    [<Literal>]
    let connectionString = @"Server=localhost;Database=SeriesDB;User=sa;Password=Senha@123"

    type Sql = SqlDataProvider<
                    ConnectionString = connectionString,
                    UseOptionTypes = true,
                    DatabaseVendor = Common.DatabaseProviderTypes.MSSQLSERVER>


    let context = Sql.GetDataContext()

    type SeriesEntity  = Sql.dataContext.``dbo.SeriesEntity``
    type EspisodesEntity  = Sql.dataContext.``dbo.EpisodesEntity``
    type Context = Sql.dataContext

