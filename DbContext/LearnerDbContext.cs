using learner.Models;
using Microsoft.EntityFrameworkCore;

namespace learner.LearnerDbContext;

public class LearnerDbContext(DbContextOptions<LearnerDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Entry> Entries => Set<Entry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entry>()
            .Property(e => e.Type)
            .HasConversion<string>();
    }
}
