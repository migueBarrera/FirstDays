using System.Threading.Tasks;

namespace FirstDays.Services.Commons
{
    public interface INavigationService
    {
        Task NavigateToLogIn();

        Task NavigateTo<T>();

        Task Back();
    }
}
