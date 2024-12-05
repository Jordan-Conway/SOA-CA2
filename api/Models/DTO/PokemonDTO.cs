namespace api.Models;

public class PokemonDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }

    public PokemonDTO(int Id, string Name, string ImageUrl)
    {
        this.Id = Id;
        this.Name = Name;
        this.ImageUrl = ImageUrl;
    }
}
