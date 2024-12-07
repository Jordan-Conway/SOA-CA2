namespace api.Models;

public class ResultItem
{
    public string? PokemonName { get; set; }
    public string? PokemonImageUrl { get; set; }
    public int VoteCount { get; set; }

    public ResultItem(string pokemonName, string pokemonImageUrl, int voteCount)
    {
        this.PokemonName = pokemonName;
        this.PokemonImageUrl = pokemonImageUrl;
        this.VoteCount = voteCount;
    }
}