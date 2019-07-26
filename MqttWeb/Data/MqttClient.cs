using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MqttWeb.SessionState;

namespace MqttWeb.Data
{
    public class MqttClient
    {
        System.Timers.Timer Timer;

        public MqttClient(MqttState mqttState)
        {
            MqttState = mqttState;
        }

        public MqttState MqttState { get; }
        public MQTTnet.Client.IMqttClient client;

        public async Task Connect()
        {
            this.MqttState.AddMessage("connecting");
            if (this.Timer == null)
            {
                this.Timer = new System.Timers.Timer();
                this.Timer.Interval = 5000;
                this.Timer.Elapsed += (o, s) =>
                {
                    this.MqttState.AddMessage("new message");
                };
                //this.Timer.Start();
                //this.MqttState.AddMessage("timer started");
            }

            var options = new MqttClientOptionsBuilder()
                .WithClientId("mqttweb")
                .WithTcpServer("mqtt.delphas.dk", 8883)
                .WithCredentials("mosquitto", "mqttmosquitto")
                .WithTls()
                .WithCleanSession()
                .Build();

            client = new MqttFactory().CreateMqttClient();


            var message = new MqttApplicationMessageBuilder()
                .WithTopic("/test")
                .WithPayload("Hello World")
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();

            
            client.UseApplicationMessageReceivedHandler(e =>
            {
                this.MqttState.AddMessage($"{e.ApplicationMessage.Topic}: {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)} ");

                //Task.Run(() => mqttClient.PublishAsync("hello/world"));
            });

            client.UseConnectedHandler(async e => {
                this.MqttState.AddMessage("Connected");
                await client.SubscribeAsync(new TopicFilterBuilder().WithTopic("/testreturn").Build());
                this.MqttState.AddMessage("Subscribed");
            });

            client.UseDisconnectedHandler(async e => {
                this.MqttState.AddMessage("Disconnected");
            });

            await client.ConnectAsync(options, CancellationToken.None);


            await client.PublishAsync(message);

        }

        public async Task Disconnect()
        {
            this.MqttState.AddMessage("Disconnecting");
            await this.client.DisconnectAsync();
            this.Timer?.Stop();
            this.Timer = null;
        }
    }
}
