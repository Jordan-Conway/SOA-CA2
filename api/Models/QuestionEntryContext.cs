using Microsoft.EntityFrameworkCore;
namespace api.Models;

public class QuestionEntryContext : DbContext
{
    public QuestionEntryContext(DbContextOptions<QuestionEntryContext> options) : base(options) { }
    public DbSet<QuestionEntry> PokemonEntries { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuestionEntry>()
            .HasMany(e => e.Pokemons)
            .WithMany(e => e.Questions)
            .UsingEntity<VoteEntry>();
    }
}