using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Services.SuperHeroServices
{
    public class SuperHeroService : ISuperHeroService
    {
        private static List<SuperHero> superHeroes = new()
        {
            new SuperHero
            {
                Id = 1,
                Name = "Spider Man",
                FirstName = "Peter",
                LastName = "Parker",
                Place = ""
            },
            new SuperHero
            {
                Id = 2,
                Name = "Batman",
                FirstName = "Bruce",
                LastName = "Wayne",
                Place = "Gotham"
            },
            new SuperHero
            {
                Id = 3,
                Name = "Iron Man",
                FirstName = "Tony",
                LastName = "Stark",
                Place = ""
            },

        };

        private readonly DataContext _context;

        public SuperHeroService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<SuperHero>?> AddHero(SuperHero hero)
        {
            if (hero is not null)
            {
                _context.SuperHeroes.Add(hero);
                //await _context.SuperHeroes.AddAsync(hero);
                await _context.SaveChangesAsync();
                
                return await GetAllHeroes();
            }
            return null;
        }

        public async Task<List<SuperHero>?> DeleteHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            try
            {
                if (hero is not null)
                {
                    _context.SuperHeroes.Remove(hero);
                    await _context.SaveChangesAsync();
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (hero is not null)
            {
                return await GetAllHeroes();
            }
            return null;
        }

        public async Task<List<SuperHero>>  GetAllHeroes() => await _context.SuperHeroes.ToListAsync();

        public async Task<SuperHero?> GetSingleHero(int heroID)
        {
            var hero = await _context.SuperHeroes.FindAsync(heroID);
            if (hero is not null)
            {
                return hero;
            }
            return null;
        }

        public async Task<List<SuperHero>?> UpdateHero(int id, SuperHero hero)
        {
            var newHero = await _context.SuperHeroes.FindAsync(id);
            if (newHero is not null)
            {
                newHero.Name = hero.Name;
                newHero.FirstName = hero.FirstName;
                newHero.LastName = hero.LastName;
                newHero.Place = hero.Place;

                await _context.SaveChangesAsync();

                return await _context.SuperHeroes.ToListAsync();
            }
            return null;
        }
    }
}
