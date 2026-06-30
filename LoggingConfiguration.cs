using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration
{
    public class LoggingConfiguration
    {
        public static void ConfigureLogging()
        {
            Directory.CreateDirectory("Logs");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(
                    "Logs/OrderLog.txt", 
                    rollingInterval: RollingInterval.Day, 
                    retainedFileCountLimit: 10,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(evt => evt.Level >= Serilog.Events.LogEventLevel.Warning)
                    .WriteTo.File(
                        "Logs/ErrorLog.txt", 
                        rollingInterval: RollingInterval.Day, 
                        retainedFileCountLimit: 30,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}--------------------------------------------------------------------------------{NewLine}"))
                .CreateLogger();
        }
    }
}
