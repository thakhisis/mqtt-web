using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MqttWeb.Data;

namespace MqttWeb.SessionState
{
    public class MqttState
    {
        public MqttConnectionSettings Settings { get; set; } = new MqttConnectionSettings();
        public event EventHandler StateChanged;

        public List<string> SubscribedTopics { get; set; } = new List<string>();
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
            // This will update any subscribers
            // that the counter state has changed
            // so they can update themselves
            // and show the current counter value
            StateChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
