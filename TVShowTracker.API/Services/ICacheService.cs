namespace TVShowTracker.API.Services
{
    //This interface defines a contract for a cache service.
    public interface ICacheService
    {
        Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> factory, TimeSpan ttl);
    }
}
