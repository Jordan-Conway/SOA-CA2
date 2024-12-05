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
    public class VoteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VoteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("cast")]
        public async Task<ActionResult<VoteEntry>> CastVote(VoteDTO vote)
        {
            var voteEntry = await _context.VoteEntry.FindAsync(vote.PokemonId, vote.QuestionId);

            if (voteEntry == null)
            {
                var pokemonEntry = await _context.PokemonEntry.FindAsync(vote.PokemonId);
                var questionEntry = await _context.QuestionEntry.FindAsync(vote.QuestionId);

                if (pokemonEntry == null || questionEntry == null)
                {
                    return NotFound();
                }

                voteEntry = new VoteEntry();
                voteEntry.PokemonId = vote.PokemonId;
                voteEntry.QuestionId = vote.QuestionId;
                voteEntry.VoteCount = 1;

                _context.VoteEntry.Add(voteEntry);
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

            voteEntry.VoteCount += 1;
            _context.Entry(voteEntry).State = EntityState.Modified;

            _context.VoteEntry.Add(voteEntry);
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

            return NoContent();
        }


        // GET: api/Vote
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoteEntry>>> GetVoteEntry()
        {
            return await _context.VoteEntry.ToListAsync();
        }

        // GET: api/Vote/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VoteEntry>> GetVoteEntry(int id)
        {
            var voteEntry = await _context.VoteEntry.FindAsync(id);

            if (voteEntry == null)
            {
                return NotFound();
            }

            return voteEntry;
        }

        // PUT: api/Vote/5
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

        // POST: api/Vote
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VoteEntry>> PostVoteEntry(VoteEntry voteEntry)
        {
            _context.VoteEntry.Add(voteEntry);
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

        // DELETE: api/Vote/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoteEntry(int id)
        {
            var voteEntry = await _context.VoteEntry.FindAsync(id);
            if (voteEntry == null)
            {
                return NotFound();
            }

            _context.VoteEntry.Remove(voteEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VoteEntryExists(int id)
        {
            return _context.VoteEntry.Any(e => e.PokemonId == id);
        }
    }
}
