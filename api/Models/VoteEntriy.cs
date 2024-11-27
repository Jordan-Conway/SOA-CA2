namespace api.Models;

public class VoteEntry
{
    public int PokemonId { get; set; }
    public int QuestionId { get; set; }
    public int VoteCount { get; set; }
}