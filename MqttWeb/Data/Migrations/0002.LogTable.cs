using FluentMigrator;

namespace MqttWeb.Data.Migrations
{
    [Migration(201908221545)]
    public class LogTable : Migration
    {
        public override void Down()
        {
            Delete.Table("Log");
        }

        public override void Up()
        {
            Create.Table("Log")
                .WithColumn("Id").AsInt64().Identity().PrimaryKey().NotNullable()
                .WithColumn("Type").AsString().NotNullable()
                .WithColumn("Created").AsDateTime2().NotNullable()
                .WithColumn("Message").AsString().NotNullable()
            ;
        }
    }
}