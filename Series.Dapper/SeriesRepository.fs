namespace Series.Data

open System.Linq
open System
open Dapper
open Dapper.Contrib
open System.Data.SqlClient


module SeriesRepository =

    let connectionString = @"Server=localhost;Database=SeriesDB;User=sa;Password=Senha@123"
    let getConnection () = 
        async {
            let conn = new SqlConnection(connectionString)
            do! conn.OpenAsync() |> Async.AwaitTask
            return conn
        }

    let addSerie (entity: Serie) = async {
        let insertSerie = """
            INSERT INTO 
            Series ( Name, Description, Status )
            OUTPUT INSERTED.Id
            VALUES ( @Name, @Description, @Status )
        """
        let insertEpisode = """
            INSERT INTO 
            Episodes ( Number, Season, Name, Description, Status, SerieId )
            VALUES (@Number, @Season, @Name, @Description, @Status, @SerieId)
        """

        use! conn = getConnection()
        let! id = conn.QuerySingleAsync<int>(insertSerie, entity) |> Async.AwaitTask

        for ep in entity.Episodes do
            do! conn.ExecuteAsync(insertEpisode, {ep with SerieId = id}) 
                |> Async.AwaitTask |> Async.Ignore

        return id
    }
 
    let getSerie (idSerie: SerieId) =
        async {
            use! conn = getConnection()
            let! serie = conn.QuerySingleAsync<Serie>("SELECT * FROM Series WHERE Id = @Id", {| Id = idSerie |}) |> Async.AwaitTask
            let! episodes = conn.QueryAsync<Episode>("SELECT * FROM Episodes WHERE SerieId = @Id", {| Id = serie.Id |}) |> Async.AwaitTask
            return { serie with Episodes = episodes |> Seq.toList }
        }
