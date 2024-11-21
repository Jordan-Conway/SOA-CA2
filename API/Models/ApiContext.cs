using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<ImageItem> Images { get; set; } = null!;
        public DbSet<QuestionItem> Questions { get; set; } = null !;
    }
}
