using AutoMapper;
using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Infrastructure.DataContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerDemo.Application.Queries.Contacts;

public record SearchContactsQuery(string SearchTerm) : IRequest<List<ContactDto>>;

public class SearchContactsQueryHandler : IRequestHandler<SearchContactsQuery, List<ContactDto>>
{
    private readonly AppDataContext _appDataContext;
    private readonly IMapper _mapper;

    public SearchContactsQueryHandler(AppDataContext appDataContext, IMapper mapper)
    {
        _appDataContext = appDataContext;
        _mapper = mapper;
    }

    public async Task<List<ContactDto>> Handle(SearchContactsQuery request, CancellationToken cancellationToken)
    {
        var contacts = await _appDataContext.Contacts
            .Where(c => c.FirstName.Contains(request.SearchTerm) || c.LastName.Contains(request.SearchTerm))
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ContactDto>>(contacts);
    }
}

