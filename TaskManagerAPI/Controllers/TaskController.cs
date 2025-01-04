using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.EntityRepositories;
using TaskManagerAPI.Models;
using TaskManagerAPI.RedisIntegration;
using TaskManagerAPI.RedisModel;


namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TaskController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEntityRepository<Employee> _employeeEntityRepository;
        private readonly IEntityRepository<Mission> _missionEntityRepository;
        private readonly IEntityRepository<Project> _projectEntityRepository;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IRedisProcess _redisProcess;

        public TaskController(AppDbContext context, IEntityRepository<Employee> EmployeeEntityRepository, IEntityRepository<Mission> missionEntityRepository, IEntityRepository<Project> projectEntityRepository, IRedisCacheService redisCacheService, IRedisProcess redisProcess)
        {
            _context = context;
            _projectEntityRepository = projectEntityRepository;
            _missionEntityRepository = missionEntityRepository;
            _employeeEntityRepository = EmployeeEntityRepository;
            _redisCacheService = redisCacheService;
            _redisProcess = redisProcess;
        }
        [HttpGet("getMissionDescription")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> GetMissionDescription(int id)
        {

            var mission = await _redisProcess.GetMissionDescribe(id);

            if (mission == null)
            {
                return BadRequest();
            }

            return Ok(mission);
        }

        [HttpGet("GetMissionForEmployee")]
        public IActionResult GetUser(int id)
        {

            var singleUser = _context.Missions.FirstOrDefault(x => x.EmployeeId == id);

            return Ok(new Mission()
            {
                Description = singleUser.Description,
                IsCompleted = singleUser.IsCompleted
            });
        }
        [HttpGet("GetMissions")]
        public async Task<IActionResult> GetMissions()
        {
            var missions = await _redisProcess.GetMissionsFromRedisProcess();

            return Ok(missions);
        } 


        [HttpPost("AddMission")]
        [Authorize(Policy = "admin")]
        public IActionResult AddMission([FromBody] Mission mission)
        {
            _missionEntityRepository.Add(mission);
            return Ok();
        }

        [HttpPost("UpdateMission")]
        [Authorize(Policy = "AdminOrSecondClass")]
        public IActionResult UpdateTask([FromBody] Mission mission)
        {
            _missionEntityRepository.Update(mission);
            return Ok();
        }


    }
}
