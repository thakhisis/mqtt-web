
namespace MqttWeb.Data
{
    public abstract class MqttClient
    {
        protected readonly MqttContextFactory contextFactory;

        public MqttClient(MqttContextFactory contextFactory) 
        {
            this.contextFactory = contextFactory;
        }
    }
}