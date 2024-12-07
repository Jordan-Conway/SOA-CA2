using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.DrivenAdapters;

public interface IDatabaseAdapter
{
    Task<ActionResult<PokemonDTO>> CreatePokemon(PokemonDTO pokemon);
    Task<ActionResult<PokemonDTO>> getRandomPokemon();
}