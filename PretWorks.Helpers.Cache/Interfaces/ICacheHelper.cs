using System;
using System.Threading.Tasks;

namespace PretWorks.Helpers.Cache.Interfaces
{
    public interface ICachehelper
    {
        /// <summary>
        /// Get or add T from the cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Key to store the item</param>
        /// <param name="getter">Func to get the item if not in cache</param>
        /// <param name="expiresInSeconds">Lifetime of the new item in cache</param>
        /// <returns></returns>
        Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> getter, int expiresInSeconds = 300);

        /// <summary>
        /// Remove item from cache
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
    }
}