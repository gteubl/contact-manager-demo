using ContactManagerDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerDemo.Infrastructure.Seeds;

public static class CitiesDataSeed
{
    public static readonly Guid BresciaId = Guid.Parse("c85b5b8f-5b67-4a2a-8f93-79f751a72c7e");
    public static readonly Guid GenovaId = Guid.Parse("60f2228a-5c90-4eec-b206-d5192c057519");
    public static readonly Guid MilanoId = Guid.Parse("9cc69728-ad13-43dd-a93b-3d733ed9e7cb");
    public static readonly Guid RomaId = Guid.Parse("e52cacbe-bcb7-4ee9-bcd3-6c7758bbcf64");
    public static readonly Guid NapoliId = Guid.Parse("2715bbd6-b877-4d92-87f1-fc5c9a003d72");
    public static readonly Guid TorinoId = Guid.Parse("57ecb213-4180-40de-bf53-dc37c74c25bb");
    public static readonly Guid FirenzeId = Guid.Parse("92fb889b-4b6e-4021-9b02-6de5e786d408");
    public static readonly Guid VeneziaId = Guid.Parse("b4f4e3de-b40f-4749-8484-82e5b072a08a");
    public static readonly Guid BolognaId = Guid.Parse("59fe6a19-1672-4d63-885e-02d167e42648");
    public static readonly Guid VeronaId = Guid.Parse("7998bdb8-d0cb-42e5-b5e0-bb4d7519cf74");

    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>().HasData(
            new City { Id = BresciaId, Name = "Brescia", Province  = "BS" },
            new City { Id = GenovaId, Name = "Genova", Province = "GE" },
            new City { Id = MilanoId, Name = "Milano", Province = "MI" },
            new City { Id = RomaId, Name = "Roma", Province = "RM" },
            new City { Id = NapoliId, Name = "Napoli", Province = "NA" },
            new City { Id = TorinoId, Name = "Torino", Province = "TO" },
            new City { Id = FirenzeId, Name = "Firenze", Province = "FI" },
            new City { Id = VeneziaId, Name = "Venezia", Province = "VE" },
            new City { Id = BolognaId, Name = "Bologna", Province = "BO" },
            new City { Id = VeronaId, Name = "Verona", Province = "VR" }
        );
    }
}
