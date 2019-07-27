namespace MqttWeb.Data
{
    public class MqttConnectionSettings {
        public string Host { get; set; } = "localhost";
        public string ClientId { get; set; } = "mac";
        public int Port { get; set; } = 1883;
        public int QoS { get; set; } = 2;
        public string Username { get; set; } = null;
        public string Password { get; set; } = null;
    }
}