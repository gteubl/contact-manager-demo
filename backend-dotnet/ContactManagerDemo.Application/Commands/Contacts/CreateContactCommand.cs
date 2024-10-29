using AutoMapper;
using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Domain.Entities;
using ContactManagerDemo.Infrastructure.DataContext;
using MediatR;

namespace ContactManagerDemo.Application.Commands.Contacts;

public record CreateContactCommand(ContactDto Contact) : IRequest<ContactDto>;

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ContactDto>
{
    private readonly AppDataContext _appDataContext;
    private readonly IMapper _mapper;

    public CreateContactCommandHandler(AppDataContext appDataContext, IMapper mapper)
    {
        _appDataContext = appDataContext;
        _mapper = mapper;
    }

    public async Task<ContactDto> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = _mapper.Map<Contact>(request.Contact);
        contact.City = null;
        await _appDataContext.Contacts.AddAsync(contact, cancellationToken);
        await _appDataContext.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ContactDto>(contact);
    }
}

