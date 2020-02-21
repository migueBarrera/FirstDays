using FirstDays.Framework;

namespace FirstDays.Services.Commons
{
    public class TaskHelperFactory
    {
        private readonly IDialogsService dialogsService;
        private readonly IConnectivityService connectivityService;

        public TaskHelperFactory(IDialogsService dialogsService, IConnectivityService connectivityService)
        {
            this.dialogsService = dialogsService;
            this.connectivityService = connectivityService;
        }

        public ITaskHelper CreateInternetAccessViewModelInstance(ILoggingService logger)
        {
            return new TaskHelper(this.dialogsService, this.connectivityService)
                .CheckInternetBeforeStarting(true)
                .WithLogging(logger);
        }

        public ITaskHelper CreateInternetAccessViewModelInstance(ILoggingService logger, BaseViewModel viewModel)
        {
            return this.CreateInternetAccessViewModelInstance(logger)
                .WhenStarting(() => viewModel.IsBusy = true)
                .WhenFinished(() => viewModel.IsBusy = false);
        }
    }
}
