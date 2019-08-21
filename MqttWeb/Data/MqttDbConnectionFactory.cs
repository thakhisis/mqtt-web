using System.Data;
using System.Data.SQLite;

namespace MqttWeb.Data {
    public class MqttDbConnectionFactory : IDbConnectionFactory {
        private readonly string connectionString;
        public MqttDbConnectionFactory (string connectionString) {
            this.connectionString = connectionString;
        }

        public IDbConnection Create() => new SQLiteConnection("Data Source=mqtt.db");
    }
}