using Microsoft.EntityFrameworkCore;

using SecretSanta.MigrationService.Models;
using SecretSanta.Models.Models;

namespace SecretSanta.MigrationService;

public class SecretSantaDBContext(DbContextOptions<SecretSantaDBContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Draw> Draws { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DrawEntry>()
        .HasOne(e => e.Giver)
        .WithMany()
        .HasForeignKey(e => e.GiverId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DrawEntry>()
            .HasOne(e => e.Receiver)
            .WithMany()
            .HasForeignKey(e => e.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

