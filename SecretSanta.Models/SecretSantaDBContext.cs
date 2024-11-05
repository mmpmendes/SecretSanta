using Microsoft.EntityFrameworkCore;

using SecretSanta.Models.Models;

namespace SecretSanta.MigrationService;

public class SecretSantaDBContext(DbContextOptions<SecretSantaDBContext> options) : DbContext(options)
{
    public DbSet<DrawEntry> Entries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}

