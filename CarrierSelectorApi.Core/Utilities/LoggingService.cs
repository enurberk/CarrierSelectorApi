using CarrierSelectorApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierSelectorApi.Core.Utilities
{
    public class LoggingService : ILoggerService
    {
        public void LogInfo(string message)
        {
            Console.WriteLine($"INFO: {message}");
        }

        public void LogError(string message)
        {
            Console.WriteLine($"ERROR: {message}");
        }
    }
}
