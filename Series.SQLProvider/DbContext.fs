namespace Series.Data

open FSharp.Data.Sql


module internal Db =

    FSharp.Data.Sql.Common.QueryEvents.SqlQueryEvent |> Event.add (printfn "Executing SQL: %O")

    [<Literal>]
    let connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=SeriesDB;Integrated Security=True;MultipleActiveResultSets=True"


    type Sql = SqlDataProvider<
                    ConnectionString = connectionString,
                    UseOptionTypes = true,
                    DatabaseVendor = Common.DatabaseProviderTypes.MSSQLSERVER
                >


    let ctx = Sql.GetDataContext()



    type SeriesEntity  = Sql.dataContext.``dbo.SeriesEntity``
    type EspisodesEntity  = Sql.dataContext.``dbo.EpisodesEntity``
    type Context = Sql.dataContext

