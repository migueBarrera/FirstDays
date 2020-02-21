using System.Collections.Concurrent;

namespace FirstDays.Services.Commons
{
    public class SessionService : ISessionService
    {
        private readonly ILoggingService logService;
        private readonly ConcurrentDictionary<string, object> cache = new ConcurrentDictionary<string, object>();

        public SessionService(ILoggingService logService)
        {
            this.logService = logService;
        }

        public T Get<T>(string key)
        {
            if (TryGet(key, out object data))
            {
                return (T)data;
            }
            else
            {
                logService.Debug($"No key {key} value found in current session");
                return default(T);
            }
        }

        public void Save<T>(string key, T data)
        {
            logService.Debug($"Saving key {key} in current session");
            cache.AddOrUpdate(key, data, (_, __) => data);
        }

        public void Remove(string key)
        {
            logService.Debug($"Removing key {key} in current session");
            cache.TryRemove(key, out object o);
        }

        public void Clear()
        {
            logService.Debug($"Clearing data from current session");
            cache.Clear();
        }

        public bool TryGet<T>(string key, out T value)
        {
            var result = cache.TryGetValue(key, out object data) && data is T;
            value = (T)data;
            return result;
        }
    }
}
