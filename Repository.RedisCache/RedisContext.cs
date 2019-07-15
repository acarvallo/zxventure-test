using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Data.Repository.RedisCache
{
    public class RedisContext 
    {
        private IRedisCacheClient redisClient;
        public RedisContext(IRedisCacheClient client)
        {
            redisClient = client;
        }
        public IRedisDatabase Database =>  redisClient.Db0;
    }
}
