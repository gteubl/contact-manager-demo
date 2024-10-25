using AutoMapper;
using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Domain.Entities;

namespace ContactManagerDemo.Application.Mappers;

public static class DtoToEntityMappers
{
    public static MapperConfiguration GetMapperConfig()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ContactDto, Contact>()
                .IgnoreCommonProperties();

            cfg.CreateMap<Contact, ContactDto>();

            
            cfg.CreateMap<CityDto, City>()
                .IgnoreCommonProperties();
            
            cfg.CreateMap<City, CityDto>();
                
        });
    }

    public static void IgnoreCommonProperties<TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> mapping)
    where TDestination : BaseEntity
    {
        mapping
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.LastUpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
    }
}