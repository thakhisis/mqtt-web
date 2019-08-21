using FluentMigrator;

namespace MqttWeb.Data.Migrations
{
    [Migration(1)]
    public class Initial : Migration
    {
        public override void Down()
        {
            Delete.Table("Configuration");
        }

        public override void Up()
        {
            Create.Table("Configuration")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("ClientId").AsString().NotNullable()
                .WithColumn("Host").AsString().NotNullable()
                .WithColumn("Port").AsCustom("int").NotNullable()
                .WithColumn("Tls").AsCustom("bit").NotNullable()
                .WithColumn("Username").AsString()
                .WithColumn("Password").AsString();
        }
    }
}