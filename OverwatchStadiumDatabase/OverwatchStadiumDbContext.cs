using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OverwatchStadiumDatabase.Models;

namespace OverwatchStadiumDatabase;

public class OverwatchStadiumDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Buff> Buffs { get; set; }
    public DbSet<ItemBuff> ItemBuffs { get; set; }
    public DbSet<Hero> Heroes { get; set; }
    public DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Buff>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<ItemBuff>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).IsRequired();

            entity.HasOne(e => e.Item).WithMany(i => i.ItemBuffs).HasForeignKey(e => e.ItemId);

            entity.HasOne(e => e.Buff).WithMany().HasForeignKey(e => e.BuffId);
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

            var imageUriProperty = entity
                .Property(e => e.ImageUri)
                .IsRequired()
                .HasConversion(uri => uri.ToString(), s => new Uri(s, UriKind.Absolute));

            imageUriProperty.Metadata.SetValueComparer(
                new ValueComparer<Uri>(
                    (a, b) => a.ToString() == b.ToString(),
                    v => v.ToString().GetHashCode(StringComparison.Ordinal),
                    v => new Uri(v.ToString(), UriKind.Absolute)
                )
            );
        });
    }
}
