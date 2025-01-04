using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;
using TaskManagerAPI.RedisIntegration;

namespace TaskManagerAPI.RedisModel
{
    
    public class RedisProcess : IRedisProcess
    {

        private readonly AppDbContext _context;
        private readonly IRedisCacheService _redisCacheService;

        public RedisProcess(AppDbContext context, IRedisCacheService redisCacheService)
        {
            _context = context;
            _redisCacheService = redisCacheService;

        }
        public async Task<List<Mission>> GetMissionsFromRedisProcess()
        {
            var cacheKey = "Missions";
            var cachedData = await _redisCacheService.GetAsync(cacheKey);

            if (cachedData != null)
            {
                var missiondeseriliazed = System.Text.Json.JsonSerializer.Deserialize<List<Mission>>(cachedData);
                return missiondeseriliazed;
            }

            var missions = await _context.Missions.ToListAsync();

            var SerializedMissions = System.Text.Json.JsonSerializer.Serialize(missions);
            await _redisCacheService.SetAsync(cacheKey, SerializedMissions);
            return missions;
        }
        public async Task<List<Project>> GetProjectsFromRedisProcess()
        {
            var cacheKey = "Projects";
            var cachedData = await _redisCacheService.GetAsync(cacheKey);
            if (cachedData != null)
            {
                var projectdeseriliazed = System.Text.Json.JsonSerializer.Deserialize<List<Project>>(cachedData);
                return projectdeseriliazed;
            }
            var projects = await _context.Projects.ToListAsync();
            var SerializedProjects = System.Text.Json.JsonSerializer.Serialize(projects);
            await _redisCacheService.SetAsync(cacheKey, SerializedProjects);
            return projects;
        }
        public async Task<Mission> GetMissionDescribe(int id)
        {
            var cacheKey = "MissionDescription";
            var cachedData = await _redisCacheService.GetAsync(cacheKey);
            if (cachedData != null)
            {
                var missiondeseriliazed = System.Text.Json.JsonSerializer.Deserialize<Mission>(cachedData);
                return missiondeseriliazed;
            }
            var mission = await _context.Missions.FirstOrDefaultAsync(x => x.Id == id);
            var SerializedMission = System.Text.Json.JsonSerializer.Serialize(mission);
            await _redisCacheService.SetAsync(cacheKey, SerializedMission);
            return mission;
        }
    }
}
