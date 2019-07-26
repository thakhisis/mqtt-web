using MQTTnet;

public interface IMqttService {
    bool Subscribe(string topic);
    bool Publish(string topic, string message, bool retain);
    bool Connect(string host, string port);
    bool Disconnect();
}

public class MqttService : IMqttService {

    public MqttService() {

    }

    public bool Connect(string host, string port)
    {
        return true;
    }

    public bool Disconnect()
    { 
        return true;
    }
  
    public bool Publish(string topic, string message, bool retain)
    {
        return true;
    }

    public bool Subscribe(string topic)
    {
        return true;
    }
}