#r "bin/Debug/netcoreapp3.1/Fsharp.Data.SqlProvider.dll"
#r "bin/Debug/netcoreapp3.1/Microsoft.Data.SqlClient.dll"

open FSharp.Data.Sql
[<Literal>]
let connectionString = @"Server=localhost;Database=SeriesDB;User=sa;Password=Senha@123"

type Sql = SqlDataProvider<
                ConnectionString = connectionString,
                UseOptionTypes = true,
                DatabaseVendor = Common.DatabaseProviderTypes.MSSQLSERVER >

let ctx = Sql.GetDataContext()
type SeriesEntity  = Sql.dataContext.``dbo.SeriesEntity``
type EspisodesEntity  = Sql.dataContext.``dbo.EpisodesEntity``


