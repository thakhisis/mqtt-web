using System;
using MQTTnet;

namespace MqttWeb.Data {
    public class MqttSubscription
    {
        public MqttSubscription(string topic)
        {
            this.Topic = topic;
        }
        
        public event EventHandler<MqttApplicationMessageReceivedEventArgs> MessageReceived;
        public string Topic { get; }

        public void MessageWasReceived(MqttApplicationMessageReceivedEventArgs e)
        {
            this.MessageReceived.Invoke(this, e);
        }
    }
}
