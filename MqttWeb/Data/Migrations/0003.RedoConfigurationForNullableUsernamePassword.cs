using FluentMigrator;

namespace MqttWeb.Data.Migrations
{
    [Migration(201908241644)]
    public class RedoConfigurationForNullableUsernamePassword : Migration
    {
        public override void Down()
        {
            Delete.FromTable("Configuration").Row(new { Username = "NULL" });
            Delete.FromTable("Configuration").Row(new { Password = "NULL" });

            Create.Table("Configuration1")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("ClientId").AsString().NotNullable()
                .WithColumn("Host").AsString().NotNullable()
                .WithColumn("Port").AsCustom("int").NotNullable()
                .WithColumn("Tls").AsCustom("bit").NotNullable()
                .WithColumn("Username").AsString().NotNullable()
                .WithColumn("Password").AsString().NotNullable();

            Execute.Sql("INSERT INTO [Configuration1] SELECT * FROM [Configuration]");

            Delete.Table("Configuration");
            Rename.Table("Configuration1").To("Configuration");
        }

        public override void Up()
        {
            Create.Table("Configuration1")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("ClientId").AsString().NotNullable()
                .WithColumn("Host").AsString().NotNullable()
                .WithColumn("Port").AsCustom("int").NotNullable()
                .WithColumn("Tls").AsCustom("bit").NotNullable()
                .WithColumn("Username").AsString().Nullable()
                .WithColumn("Password").AsString().Nullable();

            Execute.Sql("INSERT INTO [Configuration1] SELECT * FROM [Configuration]");

            Delete.Table("Configuration");
            Rename.Table("Configuration1").To("Configuration");
        }
    }
}