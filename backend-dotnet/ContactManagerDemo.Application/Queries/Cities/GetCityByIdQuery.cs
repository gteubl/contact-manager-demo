using AutoMapper;
using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Infrastructure.DataContext;
using MediatR;

namespace ContactManagerDemo.Application.Queries.Cities;

public record GetCityByIdQuery(Guid id): IRequest<CityDto?>;


public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, CityDto?>
{
    private readonly AppDataContext _appDataContext;
    private readonly IMapper _mapper;

    public GetCityByIdQueryHandler(AppDataContext appDataContext, IMapper mapper)
    {
        _appDataContext = appDataContext;
        _mapper = mapper;
    }

    public async Task<CityDto?> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await _appDataContext.Cities.FindAsync(request.id, cancellationToken);
        return city == null ? null : _mapper.Map<CityDto>(city);
    }
}
