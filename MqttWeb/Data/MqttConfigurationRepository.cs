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

        public async Task Create(Guid id, string name, string clientId, string host, int port, bool tls, string username, string password) 
        {
            using (var conn = this.dbConnectionFactory.Create()) {
                await conn.ExecuteAsync($@"INSERT INTO [Configuration] (Id, Name, ClientId, Host, Port, Tls, Username, Password) VALUES
                    (@{nameof(id)}, @{nameof(name)}, @{nameof(clientId)}, @{nameof(host)}, @{nameof(port)}, @{nameof(tls)}, @{nameof(username)}, @{nameof(password)})", 
                    new { id, name, clientId, host, port, tls, username, password });
            }
        }

        public async Task Update(Guid id, string name, string clientId, string host, int port, bool tls, string username, string password)
        {
            using (var conn = this.dbConnectionFactory.Create())
            {
                await conn.ExecuteAsync($@"
                    UPDATE [Configuration] SET 
                        Name = @{nameof(name)},
                        ClientId = @{nameof(clientId)},
                        Host = @{nameof(host)},
                        Port = @{nameof(port)},
                        Tls = @{nameof(tls)},
                        Username = @{nameof(username)},
                        Password = @{nameof(password)}
                    WHERE Id = @{nameof(id)}", new { id, name, clientId, host, port, tls, username, password });
            }
        }

        public async Task Delete(Guid id)
        {
            using (var conn = this.dbConnectionFactory.Create())
            {
                await conn.ExecuteAsync($@"DELETE FROM [Configuration] WHERE Id = @{nameof(id)}", new { id });
            }
        }

        public async Task<IEnumerable<MqttConfiguration>> GetAll () {
            using (var conn = this.dbConnectionFactory.Create ())
                return await conn.QueryAsync<MqttConfiguration>("SELECT c.* FROM [Configuration] c", new { });
        }
    }
}