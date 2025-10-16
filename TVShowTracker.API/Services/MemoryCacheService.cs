using Microsoft.Extensions.Caching.Memory;
namespace TVShowTracker.API.Services;

public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    public MemoryCacheService(IMemoryCache cache)
    {
        _cache = cache;
    }
    //Retrieves a value from the cache if it exists; otherwise, executes a factory function to generate it.
    public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> factory, TimeSpan ttl)
    {
        if (_cache.TryGetValue(key, out T cached))
            return cached;

        var result = await factory();
        _cache.Set(key, result, ttl);
        return result;
    }
}
