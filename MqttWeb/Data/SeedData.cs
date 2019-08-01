using System;

namespace MqttWeb.Data
{
    public class SeedData {
        public static void Initialize(MqttContext db)
        {
            var configuration = new MqttConfiguration() {
                Id = Guid.NewGuid(),
                Name = "localhost",
                Host = "localhost",
                Port = 1883,
                Tls = false
            };
            
            db.Configurations.Add(configuration);
            db.SaveChanges();
        }
    }       
}