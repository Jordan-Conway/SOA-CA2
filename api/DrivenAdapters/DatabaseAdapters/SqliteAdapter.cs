using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

    public async Task<ActionResult<QuestionDTO>> GetQuestion(int id)
    {
        var question = await context.QuestionEntry.FindAsync(id);
        if (question == null)
        {
            return null;
        }

        return new QuestionDTO(question.Id, question.Question, question.CreatedBy);
    }

    public async Task<ActionResult<QuestionDTO>> GetRandomQuestion()
    {
        var questions = await context.QuestionEntry.ToListAsync();
        if (questions.IsNullOrEmpty())
        {
            return null;
        }
        var random = new Random();

        var question = questions[random.Next(1, questions.Count) - 1];

        return new QuestionDTO(question.Id, question.Question, question.CreatedBy);
    }

    public async Task DeleteQuestion(int id)
    {
        var questionEntry = await context.QuestionEntry.FindAsync(id);
        if (questionEntry == null)
        {
            return;
        }

        context.QuestionEntry.Remove(questionEntry);
        await context.SaveChangesAsync();
    }

    public async Task CastVote(int pokemonId, int questionId)
    {
        var voteEntry = await context.VoteEntry.FindAsync(pokemonId, questionId);

        if (voteEntry == null)
        {
            var pokemonEntry = await context.PokemonEntry.FindAsync(pokemonId);
            var questionEntry = await context.QuestionEntry.FindAsync(questionId);

            if (pokemonEntry == null || questionEntry == null)
            {
                return;
            }

            voteEntry = new VoteEntry();
            voteEntry.PokemonId = pokemonId;
            voteEntry.QuestionId = questionId;
            voteEntry.VoteCount = 1;

            context.VoteEntry.Add(voteEntry);
            await context.SaveChangesAsync();
            return;
        }

        voteEntry.VoteCount += 1;
        context.Entry(voteEntry).State = EntityState.Modified;

        context.VoteEntry.Update(voteEntry);
        await context.SaveChangesAsync();
    }
}