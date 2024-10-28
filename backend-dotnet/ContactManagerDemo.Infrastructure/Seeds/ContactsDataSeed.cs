using ContactManagerDemo.Domain.Entities;
using ContactManagerDemo.Domain.Enums;
using ContactManagerDemo.Infrastructure.DataContext;

namespace ContactManagerDemo.Infrastructure.Seeds;

public static class SeedHelper
{
    public static void SeedTestData(this AppDataContext context)
    {
        if (!context.Contacts.Any())
        {
            context.Contacts.AddRange(
                new Contact
                {
                    Id = Guid.Parse("1773734c-5d93-48d4-8c96-6ad7e1f16d7c"),
                    FirstName = "Mario", LastName = "Rossi",
                    PhoneNumber = "+39 340 123 4567", Email = "mario.rossi@example.com", Gender = Gender.Male,
                    BirthDate = DateTime.Parse("1960-08-12"),
                    CityId = CitiesDataSeed.MilanoId  
                },
                new Contact
                {
                    Id = Guid.Parse("79da87ef-ad11-405f-9b95-0b1f597b3d92"),
                    FirstName = "Luca", LastName = "Toni",
                    PhoneNumber = "+39 331 987 6543", Email = "luca.toni@example.com", Gender = Gender.Male,
                    BirthDate = DateTime.Parse("1977-05-16"),
                    CityId = CitiesDataSeed.GenovaId
                },
                new Contact
                {
                    Id = Guid.Parse("842adc90-87d2-42f7-9c1d-18b268ad0417"),
                    FirstName = "Giovanna", LastName = "Mezzogiorno",
                    PhoneNumber = "+39 392 456 7890", Email = "giovanna.mezzogiorno@example.com", Gender = Gender.Female,
                    BirthDate = DateTime.Parse("1974-11-09"),
                    CityId = CitiesDataSeed.RomaId
                },
                new Contact
                {
                    Id = Guid.Parse("e682b5d5-a9e3-4745-b394-1081b640e4e5"),
                    FirstName = "Sophia", LastName = "Loren",
                    PhoneNumber = "+39 349 234 5678", Email = "sophia.loren@example.com", Gender = Gender.Female,
                    BirthDate = DateTime.Parse("1934-09-20"),
                    CityId = CitiesDataSeed.NapoliId
                },
                new Contact
                {
                    Id = Guid.Parse("3e67d961-0d7a-4070-b2a6-3bc5ef2e5e5c"),
                    FirstName = "Roberto", LastName = "Baggio",
                    PhoneNumber = "+39 320 678 1234", Email = "roberto.baggio@example.com", Gender = Gender.Male,
                    BirthDate = DateTime.Parse("1967-02-18"),
                    CityId = CitiesDataSeed.FirenzeId
                }
            );
            
            var random = new Random();
            
            var cityIds = new List<Guid>
            {
                CitiesDataSeed.BresciaId,
                CitiesDataSeed.GenovaId,
                CitiesDataSeed.MilanoId,
                CitiesDataSeed.RomaId,
                CitiesDataSeed.NapoliId,
                CitiesDataSeed.TorinoId,
                CitiesDataSeed.FirenzeId,
                CitiesDataSeed.VeneziaId,
                CitiesDataSeed.BolognaId,
                CitiesDataSeed.VeronaId
            };
            
            for (var i = 0; i < 1000; i++)
            {
                context.Contacts.Add(new Contact
                {
                    FirstName = $"Contact {i}",
                    LastName = $"Lastname {i}",
                    PhoneNumber = $"+39 320 678 {random.Next(1000, 9999)}", 
                    Email = $"contact{i}@example.com",
                    BirthDate = DateTime.Now.AddYears(-random.Next(20, 70)).AddDays(random.Next(0, 365)),
                    Gender = (Gender)random.Next(1, 3),
                    CityId = cityIds[random.Next(cityIds.Count)]
                });
            }

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("Database already seeded with Contacts.");
        }
    }
}

