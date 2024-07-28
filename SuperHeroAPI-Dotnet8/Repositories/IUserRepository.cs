using SuperHeroAPI_Dotnet8.Entities;

namespace SuperHeroAPI_Dotnet8.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        User GetUserByName(string username);
    }
}