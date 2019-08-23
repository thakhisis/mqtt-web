using System;

namespace MqttWeb.Data.Models
{
    public class Log
    { //(System.Int64 Id, System.String Type, System.DateTime Created, System.String Message)
        public Log(long id, string type, DateTime created, string message)
        {
            Id = id;
            Type = type;
            Created = created;
            Message = message;
        }

        public long Id { get; }
        public string Type { get; }
        public DateTime Created { get; }
        public string Message { get; }
    }
}