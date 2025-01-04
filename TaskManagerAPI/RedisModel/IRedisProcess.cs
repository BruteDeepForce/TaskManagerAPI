using TaskManagerAPI.Models;

namespace TaskManagerAPI.RedisModel
{
    public interface IRedisProcess
    {
        public Task<List<Mission>> GetMissionsFromRedisProcess();
        public Task<List<Project>> GetProjectsFromRedisProcess();
        public Task<Mission> GetMissionDescribe(int id);
    }
}
