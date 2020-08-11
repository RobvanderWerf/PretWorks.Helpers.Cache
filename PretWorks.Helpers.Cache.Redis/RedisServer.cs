using StackExchange.Redis;

namespace PretWorks.Helpers.Cache.Redis
{
    public class RedisServer : IRedisServer
    {
        private readonly ConnectionMultiplexer _redis;
        
        public RedisServer(RedisServerSettings settings)
        {
            var configuration = ConfigurationOptions.Parse(settings.Endpoints);

            _redis = ConnectionMultiplexer.Connect(configuration);
        }

        public IDatabase GetDatabase()
        {
            return _redis.GetDatabase();
        }
    }
}