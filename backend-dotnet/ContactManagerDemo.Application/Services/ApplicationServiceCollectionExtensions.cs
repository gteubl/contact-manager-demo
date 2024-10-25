using ContactManagerDemo.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ContactManagerDemo.Application.Services;

public static class ApplicationServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.TryAddScoped<ICityService, CityService>();
        
        var mapperConfig = DtoToEntityMappers.GetMapperConfig();
        var mapper = mapperConfig.CreateMapper();
        services.TryAddSingleton(mapper);
    }
    
}