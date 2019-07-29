using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MqttWeb.Data;

namespace MqttWeb.Data
{
    public class MqttState
    {
        public MqttConnectionSettings Settings { get; set; } = new MqttConnectionSettings();
        public event EventHandler StateChanged;

        public List<MqttSubscription> Subscriptions { get; set; } = new List<MqttSubscription>();
        
        public void AddSubscription(MqttSubscription subscription) 
        {
            this.Subscriptions.Add(subscription);
            StateHasChanged();
        }

        public List<string> Messages { get; set; } = new List<string>() {  };
        public List<string> Errors { get; set; } = new List<string>() {  };

        public void AddMessage(string message)
        {
            this.Messages.Add(message);
            StateHasChanged();
        }

        public void AddError(string error)
        {
            this.Errors.Add(error);
            StateHasChanged();
        }

        private void StateHasChanged()
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
