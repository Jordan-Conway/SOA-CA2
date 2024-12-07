using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.DrivenAdapters.DatabaseApaters;

public class SqliteAdapter : IDatabaseAdapter
{
    const int POKEMON_MIN_ID = 1;
    const int POKEMON_MAX_ID = 1024;
    AppDbContext context;
    public SqliteAdapter(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<ActionResult<PokemonDTO>> CreatePokemon(PokemonDTO pokemon)
    {
        var pokemonEntry = new PokemonEntry(pokemon.Id, pokemon.Name, pokemon.ImageUrl);
        context.PokemonEntry.Add(pokemonEntry);
        await context.SaveChangesAsync();

        return pokemon;
    }

    public async Task<ActionResult<PokemonDTO>> GetRandomPokemon()
    {
        var random = new Random();
        var pokemonEntry = await context.PokemonEntry.FindAsync(random.Next(POKEMON_MIN_ID, POKEMON_MAX_ID + 1));

        if (pokemonEntry == null)
        {
            return null;
        }

        return new PokemonDTO(pokemonEntry.Id, pokemonEntry.Name, pokemonEntry.ImageUrl);
    }

    public async Task<ActionResult<QuestionDTO>> CreateQuestion(QuestionDTO question)
    {
        var questionEntry = new QuestionEntry(question.Id, question.Question, question.CreatedBy);
        context.QuestionEntry.Add(questionEntry);
        await context.SaveChangesAsync();

        return question;
    }
}