using System.Threading.Tasks;

namespace $safeprojectname$.Services.Commons
{
    public interface INavigationService
    {
        Task NavigateToLogIn();

        Task NavigateTo<T>();

        Task Back();
    }
}
