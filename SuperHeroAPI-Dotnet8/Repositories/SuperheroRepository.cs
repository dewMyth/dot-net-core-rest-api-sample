using SuperHeroAPI_Dotnet8.Data;
using SuperHeroAPI_Dotnet8.Entities;

namespace SuperHeroAPI_Dotnet8.Repositories
{
    public class SuperheroRepository : ISuperheroRepository
    {

        private readonly DataContext _context;
        public SuperheroRepository(DataContext context)
        {
            _context = context;
        }

        // Get All
        public List<Superhero> GetAll()
        {
            return _context.superheroes.Distinct().ToList();
        }

        // Get using Id
        public Superhero GetById(int id)
        {
            return _context.superheroes.Find(id);
        }

        // Add
        public void Add(Superhero superhero)
        {
            _context.superheroes.Add(superhero);
            _context.SaveChanges();
        }

        // Update
        public void Update(Superhero superhero)
        {
            _context.superheroes.Update(superhero);
            _context.SaveChanges();
        }

        // Delete
        public void Delete(int id)
        {
            var superhero = _context.superheroes.Find(id);
            if (superhero != null)
            {
                _context.superheroes.Remove(superhero);
                _context.SaveChanges();
            }
        }

    }
}
