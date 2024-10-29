using ContactManagerDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerDemo.Infrastructure.Seeds;

public static class UserDataSeed
{
    public static string DefaultAdminHashedPassword = "IHdeBy+tkaYV6QdP2XaXH+MqOoa2lls5LjoiiKblL0A=";
    public static string DefaultAdminSalt = "+GvDRMdv8QK5pqTj0UY97A=="; 
    
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("f3d3f9c0-8b19-496d-9081-f708e7533ff2"),
                Username = "admin",
                HashedPassword = DefaultAdminHashedPassword,
                Salt = DefaultAdminSalt
            }
        );
    }
}