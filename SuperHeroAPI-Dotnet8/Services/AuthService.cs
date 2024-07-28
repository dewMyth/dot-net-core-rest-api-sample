using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SuperHeroAPI_Dotnet8.Entities;
using SuperHeroAPI_Dotnet8.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperHeroAPI_Dotnet8.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _config = configuration;
        }
        public User Register(UserDto addedUser)
        {
            if (addedUser.Password == null || addedUser.Username == null)
            {
                throw new Exception("Username or Password is null");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(addedUser.Password);

            var user = new User
            {
                Username = addedUser.Username,
                PasswordHash = passwordHash
            };

            _userRepository.Add(user);
            return user;

        }


        public UserLoginResponseDto Login(UserDto loggingUser)
        {
            // Get User
            var savedUser = _userRepository.GetUserByName(loggingUser.Username);

            if (savedUser.Username != loggingUser.Username)
            {
                throw new Exception("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(loggingUser.Password, savedUser.PasswordHash))
            {
                throw new Exception("Password is WRONG!");
            }

            var token = CreateToken(savedUser);

            var userLoginResponse = new UserLoginResponseDto
            {
                token = token,
                userName = loggingUser.Username,
            };

            return userLoginResponse;
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Unable to create token for the logged in user");
            }

        }

    }
}
