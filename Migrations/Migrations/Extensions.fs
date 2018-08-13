module Ext

open FluentMigrator.Builders.Create.Table
type ICreateTableWithColumnSyntax with
    member this.WithId() =
        this.WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()



type ICreateTableColumnAsTypeSyntax with
    member this.AsMaxString() =
        this.AsCustom("nvarchar(max)")


