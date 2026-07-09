using Microsoft.Extensions.Caching.Memory;

namespace FigmaMcp.Infrastructure.Caching
{
    public class MemoryCacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T? Get<T>(string key)
        {
            return _cache.TryGetValue(key, out T value) ? value : default;
        }

        public void Set<T>(string key, T value, int minutes = 10)
        {
            _cache.Set(key, value, TimeSpan.FromMinutes(minutes));
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, int minutes = 10)
        {
            if (_cache.TryGetValue(key, out T value))
            {
                return value;
            }

            value = await factory();
            Set(key, value, minutes);
            return value;
        }
    }
}
