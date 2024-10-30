using ContactManagerDemo.Domain.Entities;
using ContactManagerDemo.Domain.Enums;
using ContactManagerDemo.Infrastructure.DataContext;
using ContactManagerDemo.Infrastructure.Seeds;
using MediatR;

namespace ContactManagerDemo.Application.Commands.Seed;

public record SeedTestContactsCommand(int Qtd) : IRequest<Unit>;

public class SeedTestContactsCommandHandler : IRequestHandler<SeedTestContactsCommand, Unit>
{
    private readonly AppDataContext _appDataContext;

    public SeedTestContactsCommandHandler(AppDataContext appDataContext) => _appDataContext = appDataContext;

    public async Task<Unit> Handle(SeedTestContactsCommand request, CancellationToken cancellationToken)
    {
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
        
        var randomNames = new List<string>
        {
            "Mario", "Luca", "Giovanna", "Sophia", "Roberto", "Gianluigi", "Alessandro", "Francesca", "Paolo", "Giuseppe"
        };
        
        var randomSurnames = new List<string>
        {
            "Rossi", "Toni", "Mezzogiorno", "Loren", "Baggio", "Buffon", "Del Piero", "Neri", "Bianchi", "Verdi"
        };

        for (var i = 0; i < request.Qtd; i++)
        {
            var name = randomNames[random.Next(randomNames.Count)];
            var surname = randomSurnames[random.Next(randomSurnames.Count)];
            _appDataContext.Contacts.Add(new Contact
            {
                FirstName =$"Test.{name}{i}",
                LastName = $"{surname}",
                PhoneNumber = $"+39 320 678 {random.Next(1000, 9999)}",
                Email = $"{name}{i}.{surname}@example.com",
                BirthDate = DateTime.Now.AddYears(-random.Next(20, 70)).AddDays(random.Next(0, 365)),
                Gender = (Gender)random.Next(1, 3),
                CityId = cityIds[random.Next(cityIds.Count)]
            });
        }

        await _appDataContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}