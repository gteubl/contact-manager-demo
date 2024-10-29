using ContactManagerDemo.Domain.Entities;
using ContactManagerDemo.Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ContactManagerDemo.Infrastructure.DataContext;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contact>(
            entity =>
            {
                entity.HasQueryFilter(e => e.IsDeleted == false);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.LastUpdatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.FirstName)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.LastName)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");
            }
        );

        modelBuilder.Entity<City>(
            entity =>
            {
                entity.HasQueryFilter(e => e.IsDeleted == false);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.LastUpdatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.Name)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");
            }
        );

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.LastUpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            
            entity.HasIndex(e => e.Username)
                .IsUnique();
        });


        // Seed data
        CitiesDataSeed.Seed(modelBuilder);
        UserDataSeed.Seed(modelBuilder);
    }
}