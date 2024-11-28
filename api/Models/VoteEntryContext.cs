using Microsoft.EntityFrameworkCore;
namespace api.Models;

public class VoteEntryContext : DbContext
{
    public VoteEntryContext(DbContextOptions<VoteEntryContext> options) : base(options) { }
    public DbSet<VoteEntry> PokemonEntries { get; set; } = null!;
}