using AutoMapper;
using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Infrastructure.DataContext;
using MediatR;

namespace ContactManagerDemo.Application.Commands.Contacts;

public record UpdateContactCommand(ContactDto Contact) : IRequest<ContactDto>;

public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, ContactDto>
{
    private readonly AppDataContext _appDataContext;
    private readonly IMapper _mapper;

    public UpdateContactCommandHandler(AppDataContext appDataContext, IMapper mapper)
    {
        _appDataContext = appDataContext;
        _mapper = mapper;
    }

    public async Task<ContactDto> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var existingContact = await _appDataContext.Contacts.FindAsync(new object[] { request.Contact.Id }, cancellationToken);
        if (existingContact == null)
        {
            throw new Exception("Contact not found");
        }

        _mapper.Map(request.Contact, existingContact);
        await _appDataContext.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ContactDto>(existingContact);
    }
}

