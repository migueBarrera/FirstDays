using Xamarin.Essentials;

namespace $safeprojectname$.Services.Commons
{
    public class ConnectivityService : IConnectivityService
    {
        public bool IsThereInternet => Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
