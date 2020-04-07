using $safeprojectname$.Framework;
using $safeprojectname$.Services.Commons;
using Xamarin.Forms;

namespace $safeprojectname$
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Locator.Init();
            Locator.Resolve<INavigationService>().NavigateToLogIn();
        }

        protected override void OnSleep()
        {
            NavigationService.AppSleep();
        }

        protected override void OnResume()
        {
            NavigationService.AppResume();
        }
    }
}
