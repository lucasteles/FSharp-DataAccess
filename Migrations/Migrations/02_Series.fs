namespace Migrations
open FluentMigrator
open Ext

[<Migration(02L)>]
type SeriesMigration() =
    inherit Migration()

    override this.Up() =
        this
            .Create.Table("Series")
            .WithId()
            .WithColumn("Name").AsString(75).NotNullable()
            .WithColumn("Description").AsMaxString().Nullable()
            .WithColumn("Status").AsString(25).NotNullable()
            |> ignore

        this.Create.ForeignKey("FK_Episodes_SerieId_Series_Id")
            .FromTable("Episodes")
            .ForeignColumn("SerieId")
            .ToTable("Series")
            .PrimaryColumn("Id")
            |> ignore

    override this.Down() =
         this.Delete.ForeignKey("FK_Episodes_SerieId_Series_Id") |> ignore
         this.Delete.Table("Series") |> ignore

