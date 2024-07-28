using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_Dotnet8.Data;
using SuperHeroAPI_Dotnet8.Entities;

namespace SuperHeroAPI_Dotnet8.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        // Get User
        public User GetUserByName(string userName)
        {
            return _context.users.FirstOrDefault(userRecord => userRecord.Username == userName);
        }

        // Save User
        public void Add(User user)
        {
            _context.users.Add(user);
            _context.SaveChanges();
        }





    }
}
