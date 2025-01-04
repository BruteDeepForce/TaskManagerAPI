namespace TaskManagerAPI.RedisIntegration
{
    public interface IRedisCacheService
    {
        Task<bool> SetAsync(string key, string value);
        Task<string?> GetAsync(string key);
        Task<bool> RemoveAsync(string key);
        public void ClearAll();
    }
}
