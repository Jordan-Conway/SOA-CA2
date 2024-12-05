using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;

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

        // PUT: api/Pokemon/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPokemonEntry(int id, PokemonEntry pokemonEntry)
        {
            if (id != pokemonEntry.Id)
            {
                return BadRequest();
            }

            _context.Entry(pokemonEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PokemonEntryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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

        // DELETE: api/Pokemon/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePokemonEntry(int id)
        {
            var pokemonEntry = await _context.PokemonEntry.FindAsync(id);
            if (pokemonEntry == null)
            {
                return NotFound();
            }

            _context.PokemonEntry.Remove(pokemonEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PokemonEntryExists(int id)
        {
            return _context.PokemonEntry.Any(e => e.Id == id);
        }
    }
}
