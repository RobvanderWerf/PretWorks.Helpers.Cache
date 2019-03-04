using System;
using System.Threading.Tasks;
using PretWorks.Helpers.Cache.Interfaces;

namespace PretWorks.Helpers.Cache.Helpers
{
    public class NullCacheHelper : ICachehelper
    {
        public Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> getter, int expiresInSeconds = 300)
        {
            return getter();
        }

        public void Remove(string key)
        {
            
        }
    }
}