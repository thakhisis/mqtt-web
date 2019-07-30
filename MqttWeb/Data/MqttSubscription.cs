using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MQTTnet;

namespace MqttWeb.Data {
    public class MqttSubscription
    {
        public event EventHandler<MqttSubscriptionMessage> MessageReceived;
        public MqttSubscription(string topic)
        {
            this.Topic = topic;
            this._messages = new List<MqttSubscriptionMessage>();
        }
        
        public string Topic { get; }
        public string UrlSafeTopic => WebUtility.UrlEncode(Topic.Replace("/", "_"));
        private List<MqttSubscriptionMessage> _messages { get; set; }
        public IEnumerable<MqttSubscriptionMessage> Messages => _messages.AsEnumerable();

        public void AddMessage(MqttSubscriptionMessage message) {
            this._messages.Add(message);
            this.MessageWasReceived(message);
        }

        private void MessageWasReceived(MqttSubscriptionMessage e)
        {
            this.MessageReceived.Invoke(this, e);
        }
    }
}
