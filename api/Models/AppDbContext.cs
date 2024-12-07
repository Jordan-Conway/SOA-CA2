using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public required DbSet<PokemonEntry> PokemonEntry { get; set; }
    public required DbSet<QuestionEntry> QuestionEntry { get; set; }
    public required DbSet<VoteEntry> VoteEntry { get; set; }
    public required DbSet<UserEntry> UserEntry { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VoteEntry>()
                .HasKey(e => new { e.PokemonId, e.QuestionId });
    }
}