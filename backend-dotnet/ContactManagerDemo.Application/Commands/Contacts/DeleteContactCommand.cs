using ContactManagerDemo.Infrastructure.DataContext;
using MediatR;

namespace ContactManagerDemo.Application.Commands.Contacts;

public record DeleteContactCommand(Guid Id) : IRequest<Unit>;

public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, Unit>
{
    private readonly AppDataContext _appDataContext;

    public DeleteContactCommandHandler(AppDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public async Task<Unit> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _appDataContext.Contacts.FindAsync(new object[] { request.Id }, cancellationToken);
        if (contact != null)
        {
            _appDataContext.Contacts.Remove(contact);
            await _appDataContext.SaveChangesAsync(cancellationToken);
        }
        
        return Unit.Value;
    }
}

