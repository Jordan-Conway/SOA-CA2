namespace api.Models;

public class PokemonEntry
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }

    public PokemonEntry(int id, string name, string imageUrl)
    {
        this.Id = id;
        this.Name = name;
        this.ImageUrl = imageUrl;
    }
}