using AutoMapper;
using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Infrastructure.DataContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerDemo.Application.Queries.Contacts;

public record GetContactsQuery() : IRequest<List<ContactDto>>;

public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, List<ContactDto>>
{
    private readonly AppDataContext _appDataContext;
    private readonly IMapper _mapper;

    public GetContactsQueryHandler(AppDataContext appDataContext, IMapper mapper)
    {
        _appDataContext = appDataContext;
        _mapper = mapper;
    }

    public async Task<List<ContactDto>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
    {
        var contacts = await _appDataContext.Contacts.ToListAsync(cancellationToken);
        return _mapper.Map<List<ContactDto>>(contacts);
    }
}
