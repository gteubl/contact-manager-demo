using AutoMapper;
using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Infrastructure.DataContext;
using MediatR;

namespace ContactManagerDemo.Application.Queries.Contacts;

public record GetContactByIdQuery(Guid Id) : IRequest<ContactDto?>;

public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, ContactDto?>
{
    private readonly AppDataContext _appDataContext;
    private readonly IMapper _mapper;

    public GetContactByIdQueryHandler(AppDataContext appDataContext, IMapper mapper)
    {
        _appDataContext = appDataContext;
        _mapper = mapper;
    }

    public async Task<ContactDto?> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
    {
        var contact = await _appDataContext.Contacts.FindAsync([request.Id], cancellationToken);
        return contact == null ? null : _mapper.Map<ContactDto>(contact);
    }
}

