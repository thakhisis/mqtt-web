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

namespace MqttWeb
{
    public class TimeoutHelper
    {
        public static void SetTimeout(long ms, Action elapsed)
        {
            System.Timers.Timer t = new System.Timers.Timer();
            t.Elapsed += (o, s) =>
            {
                t.Stop();
                t.Dispose();
                t = null;

                elapsed();
            };
            t.Interval = ms;
            t.Start();
        }
    }
}
