using SuperHeroAPI_Dotnet8.Entities;

namespace SuperHeroAPI_Dotnet8.Repositories
{

    // RESTRUCTURE POINTS
    // Always use interfaces for Services & Repositories
    public interface ISuperheroRepository
    {
        void Add(Superhero superhero);
        void Delete(int id);
        List<Superhero> GetAll();
        Superhero GetById(int id);
        void Update(Superhero superhero);
    }
}