using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;

        public AuthController(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _appDbContext = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {

            var userMail = _appDbContext.Users.FirstOrDefault(x => x.Email == login.Email);
            var userPass = _appDbContext.Users.FirstOrDefault(y => y.Password == login.Password);

            if (userMail == null || userPass == null)
            {
                return BadRequest("Unauthorized Region");

            }
            var token = GenerateToken(login.Email);

            return Ok(new { Token = token });

        }
        private string GenerateToken(string email)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
