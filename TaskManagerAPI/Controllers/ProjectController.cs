using Microsoft.AspNetCore.Mvc;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {

        [HttpGet]
        public IActionResult GetProjects()

        {
            return Ok(new { Message = "Proje listesi " });


        }
    }
}
