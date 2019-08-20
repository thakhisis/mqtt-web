using System.Data;
using Microsoft.Data.Sqlite;

namespace MqttWeb.Data {
    public class MqttDbConnectionFactory : IDbConnectionFactory {
        private readonly string connectionString;
        public MqttDbConnectionFactory (string connectionString) {
            this.connectionString = connectionString;
        }

        public IDbConnection Create() => new SqliteConnection("Data Source=mqtt.db");
    }
}