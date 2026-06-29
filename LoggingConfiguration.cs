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

            .WriteTo.File("Logs/OrderLog.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10)
            .CreateLogger();

        }
    }
}
