using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.EntityRepositories;
using TaskManagerAPI.Models;


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

        public TaskController(AppDbContext context, IEntityRepository<Employee> EmployeeEntityRepository, IEntityRepository<Mission> missionEntityRepository, IEntityRepository<Project> projectEntityRepository)
        {
            _context = context;
            _projectEntityRepository = projectEntityRepository;
            _missionEntityRepository = missionEntityRepository;
            _employeeEntityRepository = EmployeeEntityRepository;
        }
        [HttpGet("getMissionDescription")]
        [Authorize(Policy = "admin")]
        public IActionResult GetTasksTitle(int id)
        {

            var tasks = _context.Missions.ToList();

            var taskControl = tasks.FirstOrDefault(x => x.Id == id);

            if (taskControl == null)
            {
                return BadRequest();
            }
            var Tasktitles = taskControl.Description;

            return Ok(Tasktitles);
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
