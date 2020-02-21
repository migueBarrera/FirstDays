using OperationResult;
using System;
using System.Threading.Tasks;

namespace FirstDays.Services.Commons
{
    public interface ITaskHelper
    {
        ITaskHelper CheckInternetBeforeStarting(bool check);

        ITaskHelper WithLogging(ILoggingService logger);

        ITaskHelper WhenStarting(Action action);

        ITaskHelper WhenFinished(Action action);

        ITaskHelper WithErrorHandling(Func<Exception, Task<bool>> handler);

        Task<Status> TryExecuteAsync(Func<Task> task);

        Task<Result<T>> TryExecuteAsync<T>(Func<Task<T>> task);
    }
}