using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TaskController : Controller
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {  _context = context; }

        [HttpGet("gettaskdescription")]
        [Authorize]
        public IActionResult GetTasksTitle(int id)
        {

            var tasks = _context.Tasks.ToList();
            
            var taskControl = tasks.FirstOrDefault(x => x.Id == id);

            if (taskControl == null)
            {
                return BadRequest();
            }
            var Tasktitles = taskControl.Description;

            return Ok(Tasktitles);
        }


    }
}
