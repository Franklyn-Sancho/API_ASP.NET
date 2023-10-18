using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyApi.Data;
using MyApi.Models;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {

        private readonly MyApiContext _context;
        private readonly IConfiguration _configuration;

        [HttpGet]
        public ActionResult<IEnumerable<Users>> GetUsers()
        {
            return _context.Users.ToList();
        }

        [HttpPost("register")]
        public ActionResult<Users> CreateUsers(Users users)
        {

            var existingUser = _context.Users.FirstOrDefault(u => u.Email == users.Email);

            if (existingUser != null)
            {
                return BadRequest("O email já está sendo usado");
            }

            users.password = BCrypt.Net.BCrypt.HashPassword(users.password);

            _context.Users.Add(users);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUsers), new { id = users.Id }, users);
        }

        public UsersController(MyApiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login(Users users)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == users.Email);

            if (user == null)
            {
                return BadRequest("Usuário não encontrado");
            }

            if (!BCrypt.Net.BCrypt.Verify(users.password, user.password))
            {
                return BadRequest("Senha incorreta");
            }

            // Gere um token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Retorne o token JWT
            return Ok(new { Token = tokenString });
        }

    }
}
