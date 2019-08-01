using System;

namespace MqttWeb.Data 
{
    public class MqttSubscription
    {
        public Guid Id { get; set; }
        public int ConfigurationId { get; set; }
        public string Topic { get; set; }
        public bool ReconnectAtStartup { get; set; }
    }
}