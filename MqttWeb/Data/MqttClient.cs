using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet.Client.Receiving;
using MQTTnet.Client.Unsubscribing;
using System.Collections.Generic;
using System.Linq;
using MqttWeb.Shared;

namespace MqttWeb.Data
{
    public class MqttClient
    {
        public MqttClient(MqttState mqttState)
        {
            MqttState = mqttState;
        }

        public MqttState MqttState { get; }
        public MQTTnet.Client.IMqttClient client;
        public bool IsConnected => this.client != null && this.client.IsConnected;

        public async Task ConnectAsync(string clientId, string host, int port, bool tls, string username, string password)
        {

            this.MqttState.AddMessage($"connecting to {MqttState.Settings.Host}");
            var options = new MqttClientOptionsBuilder()
                .WithClientId(clientId)
                .WithTcpServer(host, port)
                .WithCleanSession();

            if (!string.IsNullOrEmpty(username))
                options = options.WithCredentials(username, password);

            if (tls)
                options = options.WithTls();

            client = new MqttFactory().CreateMqttClient();

            client.UseConnectedHandler(e => {
                this.MqttState.AddMessage("Connected");
            });

            client.UseDisconnectedHandler(e => {
                this.MqttState.AddMessage("Disconnected");
            });

            client.UseApplicationMessageReceivedHandler(e =>
            {
                var subscription = MqttState.Subscriptions.SingleOrDefault(s => s.Topic == e.ApplicationMessage.Topic);
                if (subscription != null)
                {
                    subscription.AddMessage(
                        new MqttSubscriptionMessage(
                            e.ApplicationMessage.Topic, 
                            Encoding.Default.GetString(e.ApplicationMessage.Payload), 
                            DateTime.Now)
                        );
                }
            });

            var result = await client.ConnectAsync(options.Build(), CancellationToken.None);
            if (result.ResultCode != MQTTnet.Client.Connecting.MqttClientConnectResultCode.Success) {
                this.MqttState.AddMessage("Error: " + result.ReasonString);
            }
        }

        public async Task DisconnectAsync()
        {
            await this.client.DisconnectAsync();
        }

        public async Task<MqttSubscription> SubscribeAsync(string topic)
        {
            var subscription = new MqttSubscription(topic);

            await client.SubscribeAsync(topic);

            MqttState.AddSubscription(subscription);
            return subscription;
        }

        public bool IsSubscribed(string topic)
        {
            return MqttState.Subscriptions.Any(s => s.Topic == topic);
        }

        public async Task UnsubscribeAsync(string topic)
        {
            if (!IsSubscribed(topic))
                return;

            var options = new MqttClientUnsubscribeOptions();
            options.TopicFilters = new List<string> { topic };
            await client.UnsubscribeAsync(options);

            MqttState.RemoveSubscription(s => s.Topic == topic);
        }

        public async Task PublishAsync(string topic, string payload, int qos, bool retain)
        {

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic);

            if (!string.IsNullOrEmpty(payload))
                message = message.WithPayload(payload);

            if (qos == 2)
                message = message.WithExactlyOnceQoS();
            else
                message = message.WithAtLeastOnceQoS();

            if (retain)
                message = message.WithRetainFlag();

            await client.PublishAsync(message.Build());
        }
    }
}
