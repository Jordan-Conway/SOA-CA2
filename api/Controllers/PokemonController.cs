using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using api.DrivenAdapters.DatabaseApaters;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly SqliteAdapter sqliteAdapter;

        public PokemonController(AppDbContext context)
        {
            sqliteAdapter = new SqliteAdapter(context);
        }

        [HttpGet("random")]
        public async Task<ActionResult<PokemonDTO>> GetRandomPokemonEntry()
        {
            var pokemon = await sqliteAdapter.getRandomPokemon();

            if (pokemon == null)
            {
                return StatusCode(500);
            }

            return pokemon;
        }

        // Used to construct the database.
        // POST: api/Pokemon
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PokemonDTO>> PostPokemonEntry(PokemonDTO pokemon)
        {
            var pokemonResult = sqliteAdapter.CreatePokemon(pokemon);
            return CreatedAtAction("GetPokemonEntry", new { id = pokemonResult.Id }, pokemonResult);
        }
    }
}
