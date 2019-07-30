using System;

namespace MqttWeb.Data
{
    public class MqttSubscriptionMessage {
        public MqttSubscriptionMessage(string topic, string payload, DateTime received) {
            Topic = topic;
            Payload = payload;
            Received = received;
        }

        public string Topic { get; }
        public string Payload { get; }
        public DateTime Received { get; }
    }
}