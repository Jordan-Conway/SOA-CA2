using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.DrivenAdapters;

public interface IDatabaseAdapter
{
    Task<ActionResult<PokemonDTO>> CreatePokemon(PokemonDTO pokemon);
    Task<ActionResult<PokemonDTO>> GetRandomPokemon();

    Task<ActionResult<QuestionDTO>> CreateQuestion(QuestionDTO question);
    Task<ActionResult<QuestionDTO>> GetQuestion(int id);
    Task<ActionResult<QuestionDTO>> GetRandomQuestion();
    Task DeleteQuestion(int id);

    Task CastVote(int pokemonId, int questionId);
}