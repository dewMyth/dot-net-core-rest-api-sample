using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class SuperheroController : ControllerBase
    {
        // RESTRUCTURE POINTS
        // Inject the service to the controller

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
