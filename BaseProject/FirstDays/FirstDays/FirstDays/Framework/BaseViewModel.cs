using FirstDays.Services.Commons;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FirstDays.Framework
{
    public abstract class BaseViewModel : BaseBindableObject
    {
        protected readonly ILoggingService LoggingService;
        protected readonly IDialogsService DialogsService;
        private readonly IConnectivityService connectivityService;
        protected readonly INavigationService NavigationService;
        protected readonly ISessionService SessionService;
        protected readonly TaskHelperFactory TaskHelperFactory;

        private bool isBusy;
        private bool isLoaded;

        public BaseViewModel()
        {
            this.LoggingService = Locator.Resolve<ILoggingService>();
            this.DialogsService = Locator.Resolve<IDialogsService>();
            this.connectivityService = Locator.Resolve<IConnectivityService>();
            this.NavigationService = Locator.Resolve<INavigationService>();
            this.SessionService = Locator.Resolve<ISessionService>();
            this.TaskHelperFactory = new TaskHelperFactory(DialogsService, connectivityService);
        }

        public bool IsBusy
        {
            get => isBusy;
            set => SetAndRaisePropertyChanged(ref isBusy, value);
        }

        public bool IsLoaded
        {
            get => this.isLoaded;

            set => this.SetAndRaisePropertyChanged(ref this.isLoaded, value);
        }

        public ICommand FeatureNotAvailableCommand => new AsyncCommand(ShowFeatureNotAvailableAsync);

        /// <summary>
        /// Executed when ViewModel is initially loaded. For example, when user navigates to its
        /// attached page.
        /// </summary>
        /// <param name="parameter">Navigation parameter.</param>
        /// <returns>A task <see cref="Task"/> for entering operations.</returns>
        public virtual Task EnteringAsync() => Task.CompletedTask;

        /// <summary>
        /// Executed when ViewModel is leaving. For example, when user navigates from its
        /// attached page to next page in navigation stack.
        /// </summary>
        /// <returns>A task <see cref="Task"/> for leaving operations.</returns>
        public virtual Task LeavingAsync() => Task.CompletedTask;

        /// <summary>
        /// Executed when ViewModel is resuming. For example, when apps changes from background 
        /// to foreground.
        /// </summary>
        /// <param name="parameter">Navigation parameter.</param>
        /// <returns>A task <see cref="Task"/> for resuming operations.</returns>
        public virtual Task ResumingAsync() => Task.CompletedTask;

        protected Task ShowFeatureNotAvailableAsync()
        {
            return DialogsService.ShowAlertAsync(
                "Feature not available",
                string.Empty);
        }
    }
}
