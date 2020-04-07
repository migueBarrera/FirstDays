namespace $safeprojectname$.Services.Commons
{
    public interface IConnectivityService
    {
        bool IsThereInternet { get; }
    }
}
