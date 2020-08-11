using System;
using System.Text.Json;
using System.Threading.Tasks;
using PretWorks.Helpers.Cache.Interfaces;

namespace PretWorks.Helpers.Cache.Redis
{
    public class RedisCacheHelper : ICachehelper
    {
        private readonly IRedisServer _redisServer;

        public RedisCacheHelper(IRedisServer redisServer)
        {
            _redisServer = redisServer;
        }

        public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> getter, int expiresInSeconds = 300)
        {
            var database = _redisServer.GetDatabase();

            var result = await database.StringGetAsync(key);

            var jsonOptions = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            if (!string.IsNullOrWhiteSpace(result))
            {
                return JsonSerializer.Deserialize<T>(result, jsonOptions);
            }

            var data = await getter();

            if (data == null)
            {
                return default;
            }

            var jsonData = JsonSerializer.Serialize(data, jsonOptions);

            await database.StringSetAsync(key, jsonData, TimeSpan.FromSeconds(expiresInSeconds));
            
            return data;
        }

        public void Remove(string key)
        {
            var database = _redisServer.GetDatabase();

            database.KeyDelete(key);
        }
    }
}