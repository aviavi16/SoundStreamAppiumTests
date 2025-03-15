using System;
using System.IO;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Serilog;

namespace SoundStreamAppiumTests2
{
    public class TestLogger : ITestLogger
    {
        private readonly ILogger _logger;

        public TestLogger()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/test.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void LogInfo(string message)
        {
            _logger.Information(message);
            Console.WriteLine($"[INFO]: {message}");
        }

        public void LogError(Exception ex, string context)
        {
            _logger.Error(ex, context);
            Console.WriteLine($"[ERROR]: {context}: {ex.Message}");
        }
    }
}
