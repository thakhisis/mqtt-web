namespace MqttWeb.Data
{
    public class MqttSubscribeMessage {
        public MqttSubscribeMessage(string topic, string payload) {
            Topic = topic;
            Payload = payload;
        }

        public string Topic { get; }
        public string Payload { get; }
    }
}