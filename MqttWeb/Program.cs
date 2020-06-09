using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebWindows.Blazor;

namespace MqttWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // transfer to webwindow
            //var host = CreateHostBuilder(args).Build();

            ComponentsDesktop.Run<Startup>("Mqtt Web", "wwwroot/index.html");
            
            // Initialize the database
            /*var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<MqttWeb.Data.MqttContext>();
                if (db.Database.EnsureCreated())
                {
                    Data.SeedData.Initialize(db);
                }
            }
            */
            //host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseUrls("http://localhost:5001")
                        .UseUrls("https://localhost:5000")
                        .UseStartup<Startup>();
                });
    }
}
