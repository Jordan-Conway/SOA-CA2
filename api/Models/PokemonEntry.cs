namespace api.Models;

public class PokemonEntry
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public List<QuestionEntry> Questions { get; } = [];
    public List<VoteEntry> Votes { get; } = [];
}