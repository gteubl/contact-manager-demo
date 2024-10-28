using AutoMapper;
using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Application.Dto.Responses;
using ContactManagerDemo.Common.GridData;
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
            
            cfg.CreateMap<ContactsResponse, Contact>()
                .IgnoreCommonProperties();
            
            cfg.CreateMap<Contact, ContactsResponse>()
                .IgnoreGridDataItemProperties();
                
        });
    }
    
    public static void IgnoreGridDataItemProperties<TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> mapping)
    where TDestination : IGridDataItem
    {
        mapping
            .ForMember(dest => dest.Selected, opt => opt.Ignore());
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