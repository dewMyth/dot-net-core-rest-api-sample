using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SuperHeroAPI_Dotnet8.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperHeroAPI_Dotnet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _config;

        public AuthController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpPost]
        [Route("register")]
        public ActionResult<User> Register(UserDto request)
        {
            user.Username = request.Username;

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.PasswordHash = passwordHash;

            return Ok(user);

        }

        [HttpPost]
        [Route("login")]
        public ActionResult<User> Login(UserDto request) 
        {
            if (user.Username != request.Username) {
                return BadRequest("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Password is WRONG!");
            }

            var token = CreateToken(user);


            return Ok(token);
               
        }

        private string CreateToken(User user)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username), // Standard claim in .NET
                new Claim("name", user.Username), // Custom Claim/Data , usually what we use in jwt in nodejs

                // Add a role
                 new Claim(ClaimTypes.Role, "Admin")
            };

            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:secret").Value!));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

                var jwtTokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return jwtTokenString;
            }
            catch (Exception ex) {
                throw new Exception(ex.ToString());
            }

        }

    }
}
