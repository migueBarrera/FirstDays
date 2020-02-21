using FirstDays.Features.LogIn;
using FirstDays.Services.Commons;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FirstDays.Framework
{
    public static class Locator
    {
        private static IServiceProvider ServiceProvider { get; set; }

        public static void Init()
        {
            ConfigureServices(new ServiceCollection());
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            var useFakeServices = false;

            // Services
            serviceCollection.AddSingleton<IConnectivityService, ConnectivityService>();
            serviceCollection.AddSingleton<ITaskHelper, TaskHelper>();
            serviceCollection.AddSingleton<INavigationService, NavigationService>();
            serviceCollection.AddSingleton<ISessionService, SessionService>();
            serviceCollection.AddSingleton<IDialogsService, DialogsService>();

#pragma warning disable CS0162 // Unreachable code detected
            if (DefaultSettings.DebugMode)
            {
                serviceCollection.AddSingleton<ILoggingService, DebugLoggingService>();
            }
            else
            {
                serviceCollection.AddSingleton<ILoggingService, AppCenterLoggingService>();
            }
#pragma warning restore CS0162 // Unreachable code detected

            if (useFakeServices)
            {
                // TODO Add to collection fake services
            }
            else
            {
                // TODO Add to collection NOT fake services
            }

            // ViewModels
            serviceCollection.AddSingleton<LogInViewModel>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public static T Resolve<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }
}
