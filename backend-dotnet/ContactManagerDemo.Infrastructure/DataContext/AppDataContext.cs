using ContactManagerDemo.Domain.Entities;
using ContactManagerDemo.Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerDemo.Infrastructure.DataContext;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {
    }
    
    
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



        // Seed data
        CitiesDataSeed.Seed(modelBuilder);
    }
}