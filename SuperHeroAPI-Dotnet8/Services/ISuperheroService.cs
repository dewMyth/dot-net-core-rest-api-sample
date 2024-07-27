using SuperHeroAPI_Dotnet8.Entities;

namespace SuperHeroAPI_Dotnet8.Services
{
    public interface ISuperheroService
    {
        List<Superhero> GetAllSuperheroes();
        Superhero GetSuperhero(int id);
        Superhero AddSuperhero(Superhero superhero);
        Superhero updateSuperhero(int id, Superhero superhero);
        Dictionary<string,object> DeleteSuperhero(int id);
    }
}