namespace MqttWeb.Data
{
    public class MqttConnectionSettings {
        public string ClientId { get; set; } = "mac";
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 1883;
        public bool UseTls { get; set; } = false;
        public string Username { get; set; } = null;
        public string Password { get; set; } = null;
    }
}