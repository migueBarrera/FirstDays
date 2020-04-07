using $safeprojectname$.Features.LogIn;
using $safeprojectname$.Framework;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace $safeprojectname$.Services.Commons
{
    public class NavigationService : INavigationService
    {
        private readonly ILoggingService loggingService;

        public NavigationService(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }

        protected Application CurrentApplication => Application.Current;

        public Task NavigateToLogIn()
        {
            return InternalNavigateToAsync(
                typeof(LogInViewModel), 
                null, 
                clearStack: true);
        }

        public Task Back()
        {
            var cp = CurrentApplication.MainPage as NavigationPage;
            return cp.Navigation.PopAsync();
        }

        internal static void AppResume() => InnerAppLifecycle(true);

        internal static void AppSleep() => InnerAppLifecycle(false);

        private static void InnerAppLifecycle(bool forceAppearing)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                var navigationPage = Application.Current.MainPage as NavigationPage;

                if (navigationPage == null)
                {
                    return;
                }

                var page = navigationPage.CurrentPage as BaseContentPage;

                if (page == null)
                {
                    return;
                }

                if (forceAppearing)
                {
                    page.ForceOnAppearing();
                }
                else
                {
                    page.ForceOnDisappearing();
                }
            }
        }

        protected async Task InternalNavigateToAsync(Type viewModelType, object parameter, bool clearStack = false)
        {
            try
            {
                var page = CreatePage(viewModelType, parameter);

                var cp = CurrentApplication.MainPage as NavigationPage;

                if (cp == null || clearStack)
                {
                    cp = new NavigationPage(page);
                    CurrentApplication.MainPage = cp;
                }
                else
                {
                    await cp.PushAsync(page);
                }
            }
            catch (Exception e)
            {
                loggingService.Exception(e);
            }
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            var pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            return Activator.CreateInstance(pageType) as Page;
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            var pagename = viewModelType.Name.Replace("ViewModel", "Page");
            var namespaceVar = viewModelType.Namespace;
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(
                CultureInfo.InvariantCulture, 
                "{0}.{1}, {2}", 
                namespaceVar, 
                pagename, 
                viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        public Task NavigateTo<T>()
        {
            return InternalNavigateToAsync(
                typeof(T),
                null);
        }
    }
}