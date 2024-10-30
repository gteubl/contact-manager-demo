namespace ContactManagerDemo.Application.Dto;

public class CityDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Province { get; set; }
    
}