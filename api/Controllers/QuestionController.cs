using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.DrivenAdapters.DatabaseApaters;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly SqliteAdapter sqlite;

        public QuestionController(AppDbContext context)
        {
            sqlite = new SqliteAdapter(context);
        }

        // GET: api/Question/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDTO>> GetQuestionEntry(int id)
        {
            var question = await sqlite.GetQuestion(id);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        // GET: api/Question/5
        [HttpGet("random")]
        public async Task<ActionResult<QuestionDTO>> GetQuestionEntry()
        {
            var question = await sqlite.GetRandomQuestion();

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        // POST: api/Question
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionDTO>> PostQuestionEntry(QuestionDTO question)
        {
            await sqlite.CreateQuestion(question);
            return CreatedAtAction("GetQuestionEntry", new { id = question.Id }, question);
        }

        // DELETE: api/Question/5
        [HttpDelete("{id}")]
        public async Task DeleteQuestionEntry(int id)
        {
            await sqlite.DeleteQuestion(id);
        }
    }
}
