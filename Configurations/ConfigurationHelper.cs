using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SoundStreamAppiumTests2
{
    public static class ConfigurationHelper
    {
        private static IConfigurationRoot _configuration;

        static ConfigurationHelper()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static string GetScreenshotsDirectory()
        {
            bool localTesting = _configuration.GetSection("TestSettings").GetValue<bool>("LocalTesting");
            string configuredDirectory = _configuration.GetSection("TestSettings").GetValue<string>("ScreenshotsDirectory")
                                         ?? "C:/default/screenshots"; // Fallback value
            if (localTesting)
            {
                return Path.Combine(@"C:\develop\SoundStreamAppiumTests2", "output", "screenshots");
            }

            return !string.IsNullOrEmpty(configuredDirectory) ? configuredDirectory : "screenshots";
        }
    }
}

