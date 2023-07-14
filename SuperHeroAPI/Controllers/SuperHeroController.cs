using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Services.SuperHeroServices;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroService _superHeroService;
        public SuperHeroController(ISuperHeroService superHeroService)
        {
            _superHeroService = superHeroService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes() => await _superHeroService.GetAllHeroes();

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSingleHero(int id)
        {
            var hero = await _superHeroService.GetSingleHero(id);
            if (hero is not null)
            {
                return Ok(hero);
            }
            return NotFound("not exist");
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            var heroes = await _superHeroService.AddHero(hero);
            if (heroes is not null)
            {
                return Ok(heroes);
            }
            return NotFound("Superhero is null or not added.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(int id, SuperHero request)
        {
            var result = await _superHeroService.UpdateHero(id, request);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound("Superhero not found.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var result = await _superHeroService.DeleteHero(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound("Superhero not found.");
        }
    }
}
