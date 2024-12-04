namespace api.Models;

public class QuestionEntry
{
    public int Id { get; set; }
    public string? Question { get; set; }
    public string? CreatedBy { get; set; }
    public List<PokemonEntry> Pokemons { get; } = [];
    public List<VoteEntry> Votes { get; } = [];
}