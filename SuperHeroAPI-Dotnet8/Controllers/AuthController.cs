using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SuperHeroAPI_Dotnet8.Entities;
using SuperHeroAPI_Dotnet8.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperHeroAPI_Dotnet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserDto user)
        {
            return _authService.Register(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserLoginResponseDto>> Login([FromBody] UserDto user)
        {
            return _authService.Login(user);
        }
    }
}
