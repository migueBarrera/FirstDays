namespace $safeprojectname$.Services.Commons
{
    public interface ISessionService
    {
        void Save<T>(string key, T data);

        T Get<T>(string key);

        bool TryGet<T>(string key, out T value);

        void Remove(string key);

        void Clear();
    }
}
