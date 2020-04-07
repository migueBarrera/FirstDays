using $safeprojectname$.Framework;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using XFDevice = Xamarin.Forms.Device;

namespace $safeprojectname$.Services.Commons
{
    public class AppCenterLoggingService : ILoggingService
    {
        public void Initialize()
        {
            //// I know about the abbreviated form of initialize this
            //// (https://docs.microsoft.com/en-us/appcenter/sdk/getting-started/xamarin)
            //// but, I don't know why, it only works in iOS (the first one of the list maybe?)

            string appCenterSecret;

            if (XFDevice.RuntimePlatform == XFDevice.Android)
            {
                appCenterSecret = DefaultSettings.AppCenterAndroidSecret;
            }
            else if (XFDevice.RuntimePlatform == XFDevice.iOS)
            {
                appCenterSecret = DefaultSettings.AppCenteriOSSecret;
            }
            else if (XFDevice.RuntimePlatform == XFDevice.UWP)
            {
                appCenterSecret = DefaultSettings.AppCenterUWPSecret;
            }
            else
            {
                appCenterSecret = string.Empty;
            }

            AppCenter.LogLevel = Microsoft.AppCenter.LogLevel.Verbose;
            AppCenter.Start(appCenterSecret, typeof(Analytics), typeof(Crashes));
        }

        public void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine("Log Message is disabled in Release");
        }

        public void Exception(Exception exception)
        {
            Crashes.TrackError(exception);
            Analytics.TrackEvent(
                LogLevel.Exception.ToString(),
                new Dictionary<string, string> { { exception.Message, exception.StackTrace } });
        }

        public void Warning(LogLevel level, LogFeature feature, string message, string stackTrace = "")
        {
            Analytics.TrackEvent(
                LogLevel.Exception.ToString(),
                new Dictionary<string, string> { { message, stackTrace } });
        }
    }
}
