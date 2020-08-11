using StackExchange.Redis;

namespace PretWorks.Helpers.Cache.Redis
{
    public interface IRedisServer
    {
        /// <summary>
        /// Get redis database
        /// </summary>
        /// <returns></returns>
        IDatabase GetDatabase();
    }
}