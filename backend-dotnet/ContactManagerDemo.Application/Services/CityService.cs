using AutoMapper;
using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerDemo.Application.Services;

public class CityService : ICityService
{
    private readonly AppDataContext _appDataContext;
    private readonly IMapper _mapper;

    public CityService(AppDataContext appDataContext, IMapper mapper)
    {
        _appDataContext = appDataContext;
        _mapper = mapper;
    }

    public async Task<List<CityDto>> GetCitiesAsync()
    {
        var cities = await _appDataContext.Cities.ToListAsync();
        return cities.Select(city => _mapper.Map<CityDto>(city)).ToList();
        
    }
    
    public async Task<CityDto?> GetCityByIdAsync(int id)
    {
        var city = await _appDataContext.Cities.FindAsync(id);
        return city == null ? null : _mapper.Map<CityDto>(city);
    }
    
}