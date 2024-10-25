using ContactManagerDemo.Domain.Entities;
using ContactManagerDemo.Domain.Enums;

namespace ContactManagerDemo.Application.Dto;

public class ContactDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; }
    public City City { get; set; }
}