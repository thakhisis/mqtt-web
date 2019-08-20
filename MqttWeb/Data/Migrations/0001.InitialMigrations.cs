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
                .WithColumn("Id").AsString().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("ClientId").AsString().NotNullable()
                .WithColumn("Host").AsString().NotNullable()
                .WithColumn("Port").AsInt32().NotNullable()
                .WithColumn("Tls").AsBoolean().NotNullable()
                .WithColumn("Username").AsString()
                .WithColumn("Password").AsString();
        }
    }
}