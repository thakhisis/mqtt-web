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
using MqttWeb.Data;
using MqttWeb.Data.Models;

namespace MqttWeb.Services
{
    public class MqttService
    {
        public MqttService(MqttState mqttState, LogRepository logger)
        {
            this.mqttState = mqttState;
            this.logger = logger;
        }

        private readonly MqttState mqttState;
        private readonly LogRepository logger;
        public MQTTnet.Client.IMqttClient client;

        public async Task ExceptionTest()
        {
            throw new NotImplementedException("exception test");
        }

        public async Task<bool> ConnectAsync(string clientId, string host, int port, bool tls, string username, string password)
        {
            await this.logger.LogAsync("Info", $"connecting to {host}");

            var options = new MqttClientOptionsBuilder()
                .WithClientId(clientId)
                .WithTcpServer(host, port)
                .WithCleanSession();

            if (!string.IsNullOrEmpty(username))
                options = options.WithCredentials(username, password);

            if (tls)
                options = options.WithTls();
            
            client = new MqttFactory().CreateMqttClient();

            var t = new TaskCompletionSource<Boolean>();
            var ct = new CancellationTokenSource(5000); // timeout ms
            ct.Token.Register(() => { if (!t.Task.IsCompleted) t.SetResult(false); }, useSynchronizationContext: false);
            client.UseConnectedHandler(e => {
                this.logger.Log("Info", $"Connected");
                mqttState.SetConnected(true);
                t.SetResult(true);
            });

            client.UseDisconnectedHandler(e => {
                this.logger.Log("Info", $"Disconnected");
                mqttState.SetConnected(false);
            });

            client.UseApplicationMessageReceivedHandler(e =>
            {
                var subscription = mqttState.Subscriptions.SingleOrDefault(s => s.Topic == e.ApplicationMessage.Topic);
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


            try
            {
                var result = await client.ConnectAsync(options.Build(), CancellationToken.None);
                if (result.ResultCode != MQTTnet.Client.Connecting.MqttClientConnectResultCode.Success)
                {
                    await this.logger.LogAsync("Error", result.ReasonString);
                }
            } 
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message + " : " + e.StackTrace);
                await this.logger.LogAsync("Error", e.Message + " : " + e.StackTrace);
                t.SetResult(false);
            }

            return await t.Task;
        }

        public async Task DisconnectAsync()
        {
            await this.client.DisconnectAsync();
        }

        public async Task<MqttSubscription> SubscribeAsync(string topic)
        {
            var subscription = new MqttSubscription(topic);

            await client.SubscribeAsync(topic);

            mqttState.AddSubscription(subscription);
            return subscription;
        }

        public bool IsSubscribed(string topic)
        {
            return mqttState.Subscriptions.Any(s => s.Topic == topic);
        }

        public async Task UnsubscribeAsync(string topic)
        {
            if (!IsSubscribed(topic))
                return;

            var options = new MqttClientUnsubscribeOptions();
            options.TopicFilters = new List<string> { topic };
            await client.UnsubscribeAsync(options);

            mqttState.RemoveSubscription(s => s.Topic == topic);
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
