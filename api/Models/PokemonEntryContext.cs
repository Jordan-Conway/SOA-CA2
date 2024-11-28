using Microsoft.EntityFrameworkCore;
namespace api.Models;

public class PokemonEntryContext : DbContext
{
    public PokemonEntryContext(DbContextOptions<PokemonEntryContext> options) : base(options) { }
    public DbSet<PokemonEntry> PokemonEntries { get; set; } = null!;
}