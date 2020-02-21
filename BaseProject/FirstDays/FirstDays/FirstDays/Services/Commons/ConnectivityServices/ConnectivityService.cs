using Xamarin.Essentials;

namespace FirstDays.Services.Commons
{
    public class ConnectivityService : IConnectivityService
    {
        public bool IsThereInternet => Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
