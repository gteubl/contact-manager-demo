using ContactManagerDemo.Infrastructure.DataContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerDemo.Application.Commands.Seed;

public record RemoveTestContactsCommand : IRequest<Unit>;

public class RemoveTestContactsCommandHandler : IRequestHandler<RemoveTestContactsCommand, Unit>
{
    private readonly AppDataContext _appDataContext;

    public RemoveTestContactsCommandHandler(AppDataContext appDataContext) => _appDataContext = appDataContext;

    public async Task<Unit> Handle(RemoveTestContactsCommand request, CancellationToken cancellationToken)
    {
        var contacts = await _appDataContext.Contacts.Where(c => c.FirstName.StartsWith("Test.")).ToListAsync(cancellationToken);
        _appDataContext.Contacts.RemoveRange(contacts);
        await _appDataContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
