using SuperHeroAPI_Dotnet8.Entities;

namespace SuperHeroAPI_Dotnet8.Services
{
    public interface IAuthService
    {
        UserLoginResponseDto Login(UserDto loggingUser);
        User Register(UserDto addedUser);
    }
}