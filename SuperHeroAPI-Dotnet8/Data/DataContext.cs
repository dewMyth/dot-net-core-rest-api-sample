using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_Dotnet8.Entities;

namespace SuperHeroAPI_Dotnet8.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }

        // Table Name : superheroes
        public DbSet<Superhero> superheroes { get; set; }
        public DbSet<User> users { get; set; }
    }
}
