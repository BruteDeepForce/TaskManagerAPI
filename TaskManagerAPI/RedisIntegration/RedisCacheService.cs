using StackExchange.Redis;

namespace TaskManagerAPI.RedisIntegration
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabaseAsync _databaseAsync;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _databaseAsync = _connectionMultiplexer.GetDatabase();
        }

        public async Task<string?> GetAsync(string key)
        {
            return await _databaseAsync.StringGetAsync(key);
        }

        public Task<bool> RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SetAsync(string key, string value)
        {
            return await _databaseAsync.StringSetAsync(key, value);
        }

        public void ClearAll()
        {
            var endpoints = _connectionMultiplexer.GetEndPoints();
            foreach (var endpoint in endpoints)
            {
                var server = _connectionMultiplexer.GetServer(endpoint);
                server.FlushAllDatabases();
            }
        }
    }
}
