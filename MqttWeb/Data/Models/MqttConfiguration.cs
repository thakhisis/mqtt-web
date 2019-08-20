using System;
using System.Collections.Generic;

namespace MqttWeb.Data.Models {
    public class MqttConfiguration {
        public MqttConfiguration (string id, string clientId, string host, int port, bool tls, string username, string password) {
            this.Id = id;
            this.ClientId = clientId;
            this.Host = host;
            this.Port = port;
            this.Tls = tls;
            this.Username = username;
            this.Password = password;
        }

        public string Id { get; }
        public string ClientId { get; }
        public string Name { get; }
        public string Host { get; }
        public int Port { get; }
        public bool Tls { get; }
        public string Username { get; }
        public string Password { get; }

    }
}