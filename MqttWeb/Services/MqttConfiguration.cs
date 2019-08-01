using System;

namespace MqttWeb.Services
{
    public class MqttConfiguration
    {
        public MqttConfiguration(Guid id, string name, string host, int port, bool tls) {
            Id = id;
            Name = name;
            Host = host;
            Port = port;
            Tls = tls;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Host { get; }
        public int Port { get; }
        public bool Tls { get; }
    }

}