using Microsoft.EntityFrameworkCore;
namespace api.Models;

public class QuestionEntryContext : DbContext
{
    public QuestionEntryContext(DbContextOptions<QuestionEntryContext> options) : base(options) { }
    public DbSet<QuestionEntry> PokemonEntries { get; set; } = null!;
}