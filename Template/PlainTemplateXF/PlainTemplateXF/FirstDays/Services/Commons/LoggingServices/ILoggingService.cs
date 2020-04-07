using System;

namespace $safeprojectname$.Services.Commons
{
    public interface ILoggingService
    {
        void Initialize();

        void Debug(string message);

        void Warning(LogLevel level, LogFeature feature, string message, string stackTrace = "");

        void Exception(Exception exception);
    }
}
