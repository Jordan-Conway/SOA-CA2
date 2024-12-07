using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.DrivenAdapters.DatabaseApaters;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly SqliteAdapter sqliteAdapter;

        public VoteController(AppDbContext context)
        {
            sqliteAdapter = new SqliteAdapter(context);
        }

        [HttpPost("cast")]
        public async Task CastVote(VoteDTO vote)
        {
            await sqliteAdapter.CastVote(vote.PokemonId, vote.QuestionId);
        }

        [HttpGet("results")]
        public async Task<ActionResult<ResultDTO>> GetResults(int questionId)
        {
            var results = await sqliteAdapter.GetResults(questionId);

            if (results == null || results.Value.Results.IsNullOrEmpty())
            {
                return NotFound();
            }

            return results;
        }
    }
}
