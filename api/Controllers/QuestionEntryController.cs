using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.DTOs;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionEntryController : ControllerBase
    {
        private readonly QuestionEntryContext _context;

        public QuestionEntryController(QuestionEntryContext context)
        {
            _context = context;
        }

        // GET: api/QuestionEntry
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionEntry>>> GetPokemonEntries()
        {
            return await _context.PokemonEntries.ToListAsync();
        }

        // GET: api/QuestionEntry/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDTO>> GetQuestionEntry(int id)
        {
            var questionEntry = await _context.PokemonEntries.FindAsync(id);

            if (questionEntry == null)
            {
                return NotFound();
            }

            var questionDTO = new QuestionDTO();
            questionDTO.Id = questionEntry.Id;
            questionDTO.Question = questionEntry.Question;
            questionDTO.CreatedBy = questionEntry.CreatedBy;

            return questionDTO;
        }

        // PUT: api/QuestionEntry/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionEntry(int id, QuestionEntry questionEntry)
        {
            if (id != questionEntry.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionEntryExists(id))
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

        // POST: api/QuestionEntry
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionEntry>> PostQuestionEntry(QuestionEntry questionEntry)
        {
            _context.PokemonEntries.Add(questionEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionEntry", new { id = questionEntry.Id }, questionEntry);
        }

        // DELETE: api/QuestionEntry/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionEntry(int id)
        {
            var questionEntry = await _context.PokemonEntries.FindAsync(id);
            if (questionEntry == null)
            {
                return NotFound();
            }

            _context.PokemonEntries.Remove(questionEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionEntryExists(int id)
        {
            return _context.PokemonEntries.Any(e => e.Id == id);
        }
    }
}
