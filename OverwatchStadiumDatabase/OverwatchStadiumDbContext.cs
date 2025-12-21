using Microsoft.EntityFrameworkCore;
using OverwatchStadiumDatabase.Models;

namespace OverwatchStadiumDatabase;

public class OverwatchStadiumDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Buff> Buffs { get; set; }

    // public DbSet<Models.BuffType> BuffTypes { get; set; } 
    public DbSet<Hero> Heroes { get; set; }
    public DbSet<HeroExclusive> HeroExclusives { get; set; }
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

            entity.HasMany<Item>(hero => hero.Items)
                .WithMany()
                .UsingEntity<HeroExclusive>(
                    j => j
                        .HasOne(he => he.Item)
                        .WithMany()
                        .HasForeignKey(he => he.ItemId),
                    j => j
                        .HasOne(he => he.Hero)
                        .WithMany()
                        .HasForeignKey(he => he.HeroId),
                    j => { j.HasKey(he => he.Id); });
        });

        modelBuilder.Entity<HeroExclusive>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.HeroId).IsRequired();
            entity.Property(e => e.ItemId).IsRequired();
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