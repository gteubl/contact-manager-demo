using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Application.Dto.Responses;
using ContactManagerDemo.Common.GridData;
using ContactManagerDemo.Domain.Entities;
using ContactManagerDemo.Infrastructure.DataContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerDemo.Application.Queries.Contacts;

public record GetContactsQuery(GridDataRequest Filter) : IRequest<GridDataSource<ContactsResponse>>;

public class GetContactsQueryHandler
    : IRequestHandler<GetContactsQuery, GridDataSource<ContactsResponse>>
{
    private readonly AppDataContext _appDataContext;
    private readonly IMapper _mapper;

    public GetContactsQueryHandler(AppDataContext appDataContext, IMapper mapper)
    {
        _appDataContext = appDataContext;
        _mapper = mapper;
    }

    public async Task<GridDataSource<ContactsResponse>> Handle(GetContactsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _appDataContext.Contacts.AsQueryable();

        // Apply Filters
        query = query.ApplyFilters(request.Filter, (column, magicFilter) => column.ToLower() switch
        {
            nameof(Contact.FirstName) => x => x.FirstName.Contains(magicFilter),
            nameof(Contact.LastName) => x => x.LastName.Contains(magicFilter),
            nameof(Contact.Email) => x => x.Email.Contains(magicFilter),
            nameof(Contact.PhoneNumber) => x => x.PhoneNumber != null && x.PhoneNumber.Contains(magicFilter),
            _ => null
        });

        // Apply Sorting
        query = query.ApplySorting(request.Filter);

        //Select
        var data = query
            .ProjectTo<ContactsResponse>(_mapper.ConfigurationProvider)
            .AsNoTracking();

        return await data.ToGridDataSourceAsync(request.Filter);
    }
}