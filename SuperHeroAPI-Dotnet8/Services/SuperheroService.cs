using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_Dotnet8.Entities;
using SuperHeroAPI_Dotnet8.Repositories;

namespace SuperHeroAPI_Dotnet8.Services
{
    public class SuperheroService : ISuperheroService
    {
        // RESTRUCTURE POINTS
        // Inject the relevant Repositories to Service files

        private readonly ISuperheroRepository _superheroRepository;

        // Inject the IHttpContextAccessor to access HTTP request data (Ex: If we need token in service layer)
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SuperheroService(ISuperheroRepository superheroRepository, IHttpContextAccessor httpContextAccessor)
        {
            _superheroRepository = superheroRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Superhero> GetAllSuperheroes()
        {
            // Print Token to check whether Token is accessible in Service Level
            var httpContext = _httpContextAccessor.HttpContext;
            var authTokenFromReqHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();
            Console.WriteLine(authTokenFromReqHeader);

            return _superheroRepository.GetAll();
        }

        public Superhero GetSuperhero(int id)
        {
            return _superheroRepository.GetById(id);
        }

        public Superhero AddSuperhero(Superhero superhero) 
        { 
            _superheroRepository.Add(superhero);

            return superhero;
        }

        public Superhero updateSuperhero(int id, Superhero updatedSuperhero) 
        {
            var savedHero = _superheroRepository.GetById(id);
            if (savedHero != null) { 
                savedHero.Name = updatedSuperhero.Name != "" ? updatedSuperhero.Name : savedHero.Name; ;
                savedHero.FirstName = updatedSuperhero.FirstName != "" ? updatedSuperhero.FirstName : savedHero.FirstName;
                savedHero.LastName = updatedSuperhero.LastName != "" ? updatedSuperhero.LastName : savedHero.LastName;
                savedHero.Place = updatedSuperhero.Place != "" ? updatedSuperhero.Place : savedHero.Place;
            }

            _superheroRepository.Update(savedHero);

            return savedHero;

        }



        public Dictionary<string,object> DeleteSuperhero(int id) 
        {
            var deletedHero = _superheroRepository.GetById(id);
            var customOutput = new Dictionary<string, object>();

            _superheroRepository.Delete(deletedHero.Id);

            if (deletedHero != null) {
                customOutput.Add("message", "The Hero Deleted");
                customOutput.Add("hero", deletedHero);
            }

            return customOutput;
            
        }
    }
}
