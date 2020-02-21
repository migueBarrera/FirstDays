namespace FirstDays.Framework
{
    public static class DefaultSettings
    {
        // TODO pipelines variables "__ENTER_YOUR_ANDROID_APPCENTER_SECRET_HERE__";
        public const string AppCenterAndroidSecret = "1170c108-fc01-4f03-a71a-106b27010bfa";

        // "__ENTER_YOUR_IOS_APPCENTER_SECRET_HERE__";
        public const string AppCenteriOSSecret = "c4faa6cf-60e6-47c8-b666-f7b006b4584a";

        // "__ENTER_YOUR_UWP_APPCENTER_SECRET_HERE__";
        public const string AppCenterUWPSecret = "";

        public const bool DebugMode =
#if DEBUG 
            true;
#else
            false;
#endif
    }
}
