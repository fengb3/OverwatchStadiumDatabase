using Microsoft.EntityFrameworkCore;
using OverwatchStadiumDatabase.Models;

namespace OverwatchStadiumDatabase;

public class OverwatchStadiumDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Buff> Buffs { get; set; }

    // public DbSet<Models.BuffType> BuffTypes { get; set; }
    public DbSet<Hero> Heroes { get; set; }
    public DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Buff>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.BuffName).IsRequired();
            entity.Property(e => e.Value).IsRequired();
        });

        modelBuilder.Entity<Hero>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();

            entity
                .HasMany<Item>(hero => hero.Items)
                .WithMany()
                .UsingEntity(j => j.ToTable("HeroExclusives"));
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Type).IsRequired();

            entity.HasMany<Buff>(item => item.Buffs);
        });
    }
}
