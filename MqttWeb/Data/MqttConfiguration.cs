using System;
using System.Collections.Generic;

namespace MqttWeb.Data 
{
    public class MqttConfiguration 
    {
        public Guid Id { get; set; }
        public string ClientId { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool Tls { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}