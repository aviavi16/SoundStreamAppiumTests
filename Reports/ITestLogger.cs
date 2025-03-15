namespace SoundStreamAppiumTests2
{
    public interface ITestLogger
    {
        void LogInfo(string message);
        void LogError(Exception ex, string context);
    }
}