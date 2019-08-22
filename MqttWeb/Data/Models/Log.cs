using System;

namespace MqttWeb.Data 
{
    public class Log
    {
        public Log(int id, string type, DateTime created, string message)
        {
            Id = id;
            Type = type;
            Created = created;
            Message = message;
        }

        public int Id { get; }
        public string Type { get; }
        public DateTime Created { get; }
        public string Message { get; }
    }
}