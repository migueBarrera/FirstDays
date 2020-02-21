using System.Threading.Tasks;

namespace FirstDays.Services.Commons
{
    public interface IDialogsService
    {
        Task ShowAlertAsync(string title, string body);

        Task<bool> ShowAlertAsync(string title, string body, string yesText, string noText);
    }
}
