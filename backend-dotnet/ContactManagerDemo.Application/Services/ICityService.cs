using ContactManagerDemo.Application.Dto;

namespace ContactManagerDemo.Application.Services;

public interface ICityService
{
    Task<List<CityDto>> GetCitiesAsync();
    Task<CityDto?> GetCityByIdAsync(int id);
}