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
    public class VoteEntryController : ControllerBase
    {
        private readonly VoteEntryContext _context;

        public VoteEntryController(VoteEntryContext context)
        {
            _context = context;
        }

        // GET: api/VoteEntry
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoteEntry>>> GetPokemonEntries()
        {
            return await _context.PokemonEntries.ToListAsync();
        }

        // GET: api/VoteEntry/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VoteEntry>> GetVoteEntry(int id)
        {
            var voteEntry = await _context.PokemonEntries.FindAsync(id);

            if (voteEntry == null)
            {
                return NotFound();
            }

            return voteEntry;
        }

        // PUT: api/VoteEntry/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoteEntry(int id, VoteEntry voteEntry)
        {
            if (id != voteEntry.PokemonId)
            {
                return BadRequest();
            }

            _context.Entry(voteEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoteEntryExists(id))
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

        // POST: api/VoteEntry
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VoteEntry>> PostVoteEntry(VoteEntry voteEntry)
        {
            _context.PokemonEntries.Add(voteEntry);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VoteEntryExists(voteEntry.PokemonId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVoteEntry", new { id = voteEntry.PokemonId }, voteEntry);
        }

        // DELETE: api/VoteEntry/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoteEntry(int id)
        {
            var voteEntry = await _context.PokemonEntries.FindAsync(id);
            if (voteEntry == null)
            {
                return NotFound();
            }

            _context.PokemonEntries.Remove(voteEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VoteEntryExists(int id)
        {
            return _context.PokemonEntries.Any(e => e.PokemonId == id);
        }
    }
}
