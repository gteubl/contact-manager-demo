using ContactManagerDemo.Domain.Entities;
using ContactManagerDemo.Domain.Enums;

namespace ContactManagerDemo.Application.Dto;

public class ContactDto
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required Gender Gender { get; set; }
    public required string Email { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? PhoneNumber { get; set; }
    public CityDto? City { get; set; }
}