using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PokemonController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Pokemon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PokemonDTO>>> GetPokemonEntry()
        {
            var pokemonResultList = await _context.PokemonEntry.ToListAsync();
            var pokemonList = new List<PokemonDTO>();
            pokemonResultList.ForEach(pokemon =>
            {
                pokemonList.Add(new PokemonDTO(pokemon.Id, pokemon.Name, pokemon.ImageUrl));
            });
            return pokemonList;
        }

        // GET: api/Pokemon/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PokemonDTO>> GetPokemonEntry(int id)
        {
            var pokemonEntry = await _context.PokemonEntry.FindAsync(id);

            if (pokemonEntry == null)
            {
                return NotFound();
            }

            return new PokemonDTO(pokemonEntry.Id, pokemonEntry.Name, pokemonEntry.ImageUrl);
        }

        [HttpGet("random")]
        public async Task<ActionResult<PokemonDTO>> GetRandomPokemonEntry()
        {
            const int POKEMON_MIN_ID = 1;
            const int POKEMON_MAX_ID = 1024;
            var random = new Random();
            var pokemonEntry = await _context.PokemonEntry.FindAsync(random.Next(POKEMON_MIN_ID, POKEMON_MAX_ID + 1));

            if (pokemonEntry == null)
            {
                return StatusCode(500);
            }

            return new PokemonDTO(pokemonEntry.Id, pokemonEntry.Name, pokemonEntry.ImageUrl);
        }

        // POST: api/Pokemon
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PokemonEntry>> PostPokemonEntry(PokemonEntry pokemonEntry)
        {
            _context.PokemonEntry.Add(pokemonEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPokemonEntry", new { id = pokemonEntry.Id }, pokemonEntry);
        }

        private bool PokemonEntryExists(int id)
        {
            return _context.PokemonEntry.Any(e => e.Id == id);
        }
    }
}
