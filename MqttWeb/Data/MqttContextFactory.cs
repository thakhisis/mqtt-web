

 using Microsoft.EntityFrameworkCore;

namespace MqttWeb.Data
{
    public class MqttContextFactory
    {
        public MqttContextFactory(DbContextOptions options)
        {
            Options = options;
        }

        public DbContextOptions Options { get; }

        public MqttContext Create()
        {
            return new MqttContext(Options);
        }
    }
}

