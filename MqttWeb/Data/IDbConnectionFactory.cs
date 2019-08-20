
using System.Data;

namespace MqttWeb.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection Create();
    }
}