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

namespace MqttWeb.Services
{
    public class MqttService
    {
        public MqttService(MqttState mqttState, MqttConfigurationRepository configurationRepository)
        {
            this.mqttState = mqttState;
            this.configurationRepository = configurationRepository;
        }

        private readonly MqttState mqttState;
        public MQTTnet.Client.IMqttClient client;
        private readonly MqttConfigurationRepository configurationRepository;

        public async Task CreateConfiguration(MqttConfiguration configuration) 
        {
            await this.configurationRepository.Create(configuration.Name, configuration.ClientId, configuration.Host, configuration.Port, configuration.Tls, configuration.Username, configuration.Password);
        }

        public async Task UpdateConfiguration(MqttConfiguration configuration) 
        {
            //await this.configurationRepository.Update(configuration.Id, configuration.Name, configuration.Host, configuration.Port, configuration.Tls, configuration.Username, configuration.Password);
        }

        public async Task<IEnumerable<Services.MqttConfiguration>> GetConfigurations()
        {
            
            return (await this.configurationRepository.GetAll()).Select(mc => new Services.MqttConfiguration { Id = System.Guid.Parse(mc.Id), Name = mc.Name, Host = mc.Host, Port = mc.Port, Tls = mc.Tls });
        }

        public async Task ConnectAsync(string clientId, string host, int port, bool tls, string username, string password)
        {

            this.mqttState.AddMessage($"connecting to {host}");
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
                this.mqttState.AddMessage("Connected");
                mqttState.SetConnected(true);
            });

            client.UseDisconnectedHandler(e => {
                this.mqttState.AddMessage("Disconnected");
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

            var result = await client.ConnectAsync(options.Build(), CancellationToken.None);
            if (result.ResultCode != MQTTnet.Client.Connecting.MqttClientConnectResultCode.Success) {
                this.mqttState.AddMessage("Error: " + result.ReasonString);
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
