using System;
using System.IO;
using System.Threading;
using Serilog;
using Allure.Commons;

namespace SoundStreamAppiumTests2
{
    public static class Logger
    {
        private static readonly object _lock = new object(); // Ensures thread safety

        static Logger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        /// <summary>
        /// Logs an exception and reports it to Allure.
        /// </summary>
        public static void LogError(Exception ex, string context)
        {
            AllureLifecycle.Instance.UpdateStep(step =>
            {
                step.name = context;
                step.status = Allure.Commons.Status.failed; 
            });

            Console.WriteLine($"[ERROR]: {ex.Message}");
        }

        /// <summary>
        /// Logs information messages.
        /// </summary>
        public static void LogInfo(string message)
        {
            lock (_lock)
            {
                Log.Information(message);
                Console.WriteLine($"[INFO]: {message}");
            }
        }

        /// <summary>
        /// Logs warnings.
        /// </summary>
        public static void LogWarning(string message)
        {
            lock (_lock)
            {
                Log.Warning(message);
                Console.WriteLine($"[WARNING]: {message}");
            }
        }

        /// <summary>
        /// Logs bad parsing errors.
        /// </summary>
        public static void LogParsingError(Exception ex, string rawData)
        {
            lock (_lock)
            {
                string errorDetails = $"Parsing Error: {ex.Message}\nRaw Data: {rawData}";
                Log.Error(ex, errorDetails);

                // Generate a unique step ID
                string stepId = Guid.NewGuid().ToString();

                // Create the step result (Notice that step.id is NOT set here)
                var step = new StepResult
                {
                    name = "Parsing Error",
                    status = Allure.Commons.Status.failed,
                    description = $"Exception: {ex.Message}\nRaw Data: {rawData}"
                };

                // 🔥 IMPORTANT: We pass stepId separately because Allure requires the UUID explicitly.
                // Even though StepResult has an `id` field, Allure ignores it in `StartStep()`.
                // AllureLifecycle.Instance.StartStep(uuid, step) is the correct way to start a step.
                AllureLifecycle.Instance.StartStep(stepId, step);
                AllureLifecycle.Instance.StopStep(stepId);

                Console.WriteLine($"[PARSING ERROR]: {ex.Message} | Data: {rawData}");
            }
        }
    }
}
