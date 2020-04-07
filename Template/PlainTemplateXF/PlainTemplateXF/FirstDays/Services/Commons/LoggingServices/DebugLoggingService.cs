using System;

namespace $safeprojectname$.Services.Commons
{
    public class DebugLoggingService : ILoggingService
    {
        public void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Exception(Exception exception)
        {
            Debug($"### {exception.Message}");
            Debug(exception.StackTrace);
        }

        public void Initialize()
        {
        }

        public void Warning(LogLevel level, LogFeature feature, string message, string stackTrace = "")
        {
            Debug($"# {nameof(Warning)}");
            Debug(message);
        }
    }
}
