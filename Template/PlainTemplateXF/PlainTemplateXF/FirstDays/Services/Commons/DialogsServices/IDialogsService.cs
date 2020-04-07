using System.Threading.Tasks;

namespace $safeprojectname$.Services.Commons
{
    public interface IDialogsService
    {
        Task ShowAlertAsync(string title, string body);

        Task<bool> ShowAlertAsync(string title, string body, string yesText, string noText);
    }
}
