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
    public class QuestionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuestionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Question
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionEntry>>> GetQuestionEntry()
        {
            return await _context.QuestionEntry.ToListAsync();
        }

        // GET: api/Question/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionEntry>> GetQuestionEntry(int id)
        {
            var questionEntry = await _context.QuestionEntry.FindAsync(id);

            if (questionEntry == null)
            {
                return NotFound();
            }

            return questionEntry;
        }

        // PUT: api/Question/5
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

        // POST: api/Question
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionEntry>> PostQuestionEntry(QuestionEntry questionEntry)
        {
            _context.QuestionEntry.Add(questionEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionEntry", new { id = questionEntry.Id }, questionEntry);
        }

        // DELETE: api/Question/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionEntry(int id)
        {
            var questionEntry = await _context.QuestionEntry.FindAsync(id);
            if (questionEntry == null)
            {
                return NotFound();
            }

            _context.QuestionEntry.Remove(questionEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionEntryExists(int id)
        {
            return _context.QuestionEntry.Any(e => e.Id == id);
        }
    }
}
