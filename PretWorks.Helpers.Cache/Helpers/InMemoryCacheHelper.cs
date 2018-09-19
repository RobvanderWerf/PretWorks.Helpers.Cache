using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using PretWorks.Helpers.Cache.Interfaces;

namespace PretWorks.Helpers.Cache.Helpers
{
    public class InMemoryCacheHelper : ICachehelper
    {
        private readonly IMemoryCache _cache;

        public InMemoryCacheHelper(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> getter, int expiresInSeconds = 300)
        {
            if (_cache.TryGetValue(key, out T result))
            {
                return result;
            }

            result = await getter();

            if (result == null)
            {
                return default;
            }

            _cache.Set(key, result, new TimeSpan(0, 0, expiresInSeconds));

            return result;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}