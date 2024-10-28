using AutoMapper;
using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Infrastructure.DataContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerDemo.Application.Queries.Cities;

public record GetCitiesQuery : IRequest<List<CityDto>>;


public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, List<CityDto>>
{
    private readonly AppDataContext _appDataContext;
    private readonly IMapper _mapper;

    public GetCitiesQueryHandler(AppDataContext appDataContext, IMapper mapper)
    {
        _appDataContext = appDataContext;
        _mapper = mapper;
    }


    public async Task<List<CityDto>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        var cities = await _appDataContext.Cities.ToListAsync(cancellationToken: cancellationToken);
        return cities.Select(city => _mapper.Map<CityDto>(city)).ToList();
    }
}