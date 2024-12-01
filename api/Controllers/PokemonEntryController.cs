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
    public class PokemonEntryController : ControllerBase
    {
        private readonly PokemonEntryContext _context;

        public PokemonEntryController(PokemonEntryContext context)
        {
            _context = context;
        }

        // GET: api/PokemonEntry
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PokemonEntry>>> GetPokemonEntries()
        {
            return await _context.PokemonEntries.ToListAsync();
        }

        // GET: api/PokemonEntry/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PokemonEntry>> GetPokemonEntry(int id)
        {
            var pokemonEntry = await _context.PokemonEntries.FindAsync(id);

            if (pokemonEntry == null)
            {
                return NotFound();
            }

            return pokemonEntry;
        }

        //Get: api/PokemonEntry/random
        [HttpGet("random")]
        public async Task<ActionResult<PokemonEntry>> GetRandomPokemonEntry()
        {
            const int MIN_ID = 1;
            const int MAX_ID = 1024;

            var random = new Random();
            var id = random.Next(MIN_ID, MAX_ID + 1);
            var pokemonEntry = await _context.PokemonEntries.FindAsync(id);

            if (pokemonEntry == null)
            {
                return NotFound();
            }

            return pokemonEntry;
        }


        // PUT: api/PokemonEntry/5
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

        // POST: api/PokemonEntry
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PokemonEntry>> PostPokemonEntry(PokemonEntry pokemonEntry)
        {
            _context.PokemonEntries.Add(pokemonEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPokemonEntry", new { id = pokemonEntry.Id }, pokemonEntry);
        }

        // DELETE: api/PokemonEntry/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePokemonEntry(int id)
        {
            var pokemonEntry = await _context.PokemonEntries.FindAsync(id);
            if (pokemonEntry == null)
            {
                return NotFound();
            }

            _context.PokemonEntries.Remove(pokemonEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PokemonEntryExists(int id)
        {
            return _context.PokemonEntries.Any(e => e.Id == id);
        }
    }
}
