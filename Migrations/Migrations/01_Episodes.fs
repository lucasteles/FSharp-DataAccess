namespace Migrations
open FluentMigrator
open Ext

[<Migration(01L)>]
type EpisodesMigration() =
    inherit Migration()

    override this.Up() =
        this
            .Create.Table("Episodes")
            .WithId()
            .WithColumn("SerieId").AsInt32().NotNullable()
            .WithColumn("Number").AsInt32().NotNullable()
            .WithColumn("Season").AsInt32().NotNullable()
            .WithColumn("Name").AsString(75).NotNullable()
            .WithColumn("Description").AsMaxString().NotNullable()
            .WithColumn("Status").AsString(25).NotNullable()
            |> ignore


    override this.Down() =
         this.Delete.Table("Episodes")|> ignore

