using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;
using TaskManagerAPI.Register_Model;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _register;

        public RegisterController(IRegisterService register)
        {
            _register = register;
        }   

        [HttpPost("RegisterLink")]
        [Authorize(Policy = "admin")]
        public IActionResult Register([FromBody] Employee employee)
        {
            _register.RegisterOp(employee);
            return Ok();
        }
    }
}
