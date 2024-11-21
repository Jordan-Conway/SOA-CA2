using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionItemsController : ControllerBase
    {
        private readonly ApiContext _context;

        public QuestionItemsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/QuestionItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionItem>>> GetQuestionItems()
        {
            if(_context.Questions == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions.Select(t =>
                new QuestionDTO()
                {
                    Id = t.Id,
                    Text = t.Text,
                    Author = t.Author
                }
            ).ToListAsync();

            return Ok(questions);
        }

        // GET: api/QuestionItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionItem>> GetQuestionItem(Guid id)
        {
            var questionItem = await _context.Questions.FindAsync(id);

            if (questionItem == null)
            {
                return NotFound();
            }

            return questionItem;
        }

        // PUT: api/QuestionItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionItem(int id, QuestionItem questionItem)
        {
            if (id != questionItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionItemExists(id))
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

        // POST: api/QuestionItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionItem>> PostQuestionItem(QuestionItem questionItem)
        {
            _context.Questions.Add(questionItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuestionItem), new { id = questionItem.Id }, questionItem);
        }

        // DELETE: api/QuestionItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionItem(Guid id)
        {
            var questionItem = await _context.Questions.FindAsync(id);
            if (questionItem == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(questionItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionItemExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
