
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MqttWeb.Data
{
    public class MqttConfigurationRepository : MqttClient
    {

        public MqttConfigurationRepository(MqttContextFactory contextFactory) : base(contextFactory)
        {
        }
 
        public async Task Create(string name, string host, int port, bool tls, string username, string password) 
        {

            var id = Guid.NewGuid();
            using (var conn = base.contextFactory.Create()) {
                await conn.Configurations.AddAsync(new MqttConfiguration() { Id = id, Name = name, Host = host, Port = port, Tls = tls, Username = username, Password = password});
                await conn.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MqttConfiguration>> GetAll()
        {
            using (var conn = base.contextFactory.Create())
                return await conn.Configurations.ToListAsync();
        }
    }
}