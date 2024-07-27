using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_Dotnet8.Data;
using SuperHeroAPI_Dotnet8.Entities;
using SuperHeroAPI_Dotnet8.Services;

namespace SuperHeroAPI_Dotnet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperheroController : ControllerBase
    {
        //private readonly DataContext _context;
        //public SuperheroController(DataContext context) 
        //{ 
        //    _context = context;
        //}

        // [HttpGet]
        // If needs to return a Generic Type (instead of SuperHero type), use ActionResult Interface (Task<IActionResult>)
        //public async Task<ActionResult<List<Superhero>>> GetAllHeroes()
        //{
        /* 
         * 1 . _context is the db object
         * 2. superheroes is the table name 
         */
        //var heroes = await _context.superheroes.ToListAsync();
        // return Ok(heroes);
        // }

        //[HttpGet]
        //[Route("getSuperHeroById/{id}")]
        //public async Task<ActionResult<Superhero>> GetHeroById(int id)
        //{
        //    var theHero = await _context.superheroes.FindAsync(id);
        //    if (theHero == null) { 
        //        return NoContent();
        //    }
        //    else
        //    {
        //        return Ok(theHero);
        //    }

        //}

        //[HttpGet]
        //[Route("getSuperHeroByFirstName/{fName}")]
        //public async Task<ActionResult<Superhero>> GetHeroByFirstName(string fName)
        //{
        //    var theHero = await _context.superheroes.FirstOrDefaultAsync(x => x.FirstName == fName);
        //    return Ok(theHero);
        //}

        //[HttpPost]
        //[Route("addSuperhero")]
        //public async Task<ActionResult<Superhero>> AddSuperhero([FromBody] Superhero hero)
        //{
        //    //Just Create the table save ready entity 
        //   _context.superheroes.Add(hero);

        //    // This saves the DB changes
        //    await _context.SaveChangesAsync();

        //    return Ok(hero);
        //}

        //[HttpPut]
        //[Route("editSuperheroById/{id}")]
        //public async Task<ActionResult<Superhero>> EditSuperhero(int id, [FromBody] Superhero updatedHero)
        //{
        //    var savedHero = await _context.superheroes.FindAsync(id);

        //    if(savedHero == null) { return Ok("No Heroes found with given parameters!"); }

        //    savedHero.Name = updatedHero.Name != "" ? updatedHero.Name : savedHero.Name;
        //    savedHero.FirstName = updatedHero.FirstName != "" ? updatedHero.FirstName : savedHero.FirstName;
        //    savedHero.LastName = updatedHero.LastName != "" ? updatedHero.LastName : savedHero.LastName;
        //    savedHero.Place = updatedHero.Place != "" ? updatedHero.Place : savedHero.Place;

        //    await _context.SaveChangesAsync();

        //    return Ok(savedHero);
        //}

        //[HttpDelete]
        //[Route("deleteSuperheroById/{id}")]
        //public async Task<IActionResult> deleteSuperHero(int id)
        //{
        //    var theHero = await _context.superheroes.FindAsync(id);

        //    var output = new Dictionary<string, object>();

        //    output.Add("hero", theHero);
        //    output.Add("message", "DELETED!");

        //    _context.superheroes.Remove(theHero);
        //    await _context.SaveChangesAsync();

        //    return Ok(output);
        //}



        // Using Proper Code Structure
        private readonly ISuperheroService _superheroService;

        public SuperheroController(ISuperheroService superheroService)
        {
            _superheroService = superheroService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Superhero>>> GetAllSuperheroes()
        {
            return _superheroService.GetAllSuperheroes();
        }

        [HttpGet]
        [Route("getSuperHeroById/{id}")]
        public async Task<ActionResult<Superhero>> GetSuperheroById(int id)
        {
            return _superheroService.GetSuperhero(id);
        }

        [HttpPost]
        [Route("addSuperhero")]
        public async Task<ActionResult<Superhero>> AddSuperhero([FromBody] Superhero hero)
        {
            return _superheroService.AddSuperhero(hero);
        }

        [HttpPut]
        [Route("editSuperheroById/{id}")]
        public async Task<ActionResult<Superhero>> EditSuperhero(int id, [FromBody] Superhero hero)
        {
            return _superheroService.updateSuperhero(id, hero);
        }

        [HttpDelete]
        [Route("deleteSuperheroById/{id}")]
        public async Task<ActionResult<Dictionary<string,object>>> DeleteSuperhero(int id)
        {
            return _superheroService.DeleteSuperhero(id);
        }

    }
}
