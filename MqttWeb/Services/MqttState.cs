using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MqttWeb.Data;

namespace MqttWeb.Services
{
    public class MqttState
    {
        public MqttConnectionSettings Settings { get; set; } = new MqttConnectionSettings();
        public event EventHandler StateChanged;

        private List<MqttSubscription> _subscriptions { get; set; } = new List<MqttSubscription>();
        public IEnumerable<MqttSubscription> Subscriptions => _subscriptions.AsEnumerable();
        
        public bool IsConnected { get; private set; }
        public void SetConnected(bool connected) 
        {
            this.IsConnected = connected;
            this.StateHasChanged();
        }

        public void AddSubscription(MqttSubscription subscription) 
        {
            subscription.MessageReceived += (o, s) => StateHasChanged();

            this._subscriptions.Add(subscription);

            StateHasChanged();
        }

        public void RemoveSubscription(Predicate<MqttSubscription> query)
        {
            this._subscriptions.RemoveAll(query);
        }

        public bool IsSubscribedTo(string topic) => this._subscriptions.Any(s => s.Topic == topic);


        public List<string> Messages { get; set; } = new List<string>() {  };

        public void AddMessage(string message) 
        {
            this.Messages.Add(message);
            this.StateHasChanged();
        }

        public void StateHasChanged()
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
