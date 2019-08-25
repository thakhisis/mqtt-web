using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MqttWeb.Data.Models;

namespace MqttWeb.Data {
    public class LogRepository 
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public LogRepository(IDbConnectionFactory dbConnectionFactory) {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public void Log(string type, string message, DateTime? created = null)
        {
            Task.Run(() => LogAsync(type, message, created));
        }

        public async Task LogAsync(string type, string message, DateTime? created = null) 
        {
            if (!created.HasValue) created = DateTime.Now;
            try
            {

                using (var conn = this.dbConnectionFactory.Create())
                {
                    await conn.ExecuteAsync($@"INSERT INTO [Log] ([Type], [Created], [Message]) VALUES
                    (@{nameof(type)}, @{nameof(created)}, @{nameof(message)})",
                        new { type, message, created });
                }
            } 
            catch (Exception e)
            {
                Console.Error.WriteLine("");
                Console.Error.WriteLine("");
                Console.Error.WriteLine(" EXCEPTION IN LOGGING ");
                Console.Error.WriteLine("");
                Console.Error.WriteLine("");
                Console.Error.WriteLine($"{e.Message} :: {e.StackTrace}");
                Console.Error.WriteLine("");
                Console.Error.WriteLine("");

            }
        }

        public async Task<IEnumerable<Log>> GetAll () {
            using (var conn = this.dbConnectionFactory.Create ())
            {
                var norestul = await conn.QueryAsync<Log>("SELECT l.* FROM [Log] l", new { });
                return norestul;
            }
        }
    }
}