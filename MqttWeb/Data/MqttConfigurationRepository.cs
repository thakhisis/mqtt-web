using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MqttWeb.Data.Models;

namespace MqttWeb.Data {
    public class MqttConfigurationRepository //: MqttClient
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public MqttConfigurationRepository (IDbConnectionFactory dbConnectionFactory) {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task Create (string name, string clientId, string host, int port, bool tls, string username, string password) {

            var id = Guid.NewGuid ();
            using (var conn = this.dbConnectionFactory.Create()) {
                await conn.ExecuteAsync($@"INSERT INTO [Configuration] (Id, Name, ClientId, Host, Port, Tls, Username, Password) VALUES
                    (@{nameof(id)}, @{nameof(name)}, @{nameof(clientId)}, @{nameof(host)}, @{nameof(port)}, @{nameof(tls)}, @{nameof(username)}, @{nameof(password)})", 
                    new { id, name, clientId, host, port, tls, username, password });
            }
        }

        public async Task Update (Guid id, string name, string host, int port, bool tls, string username, string password) {

        }

        public async Task<IEnumerable<MqttConfiguration>> GetAll () {
            using (var conn = this.dbConnectionFactory.Create ())
                return await conn.QueryAsync<MqttConfiguration>("SELECT c.* FROM [Configuration] c", new { });
        }
    }
}