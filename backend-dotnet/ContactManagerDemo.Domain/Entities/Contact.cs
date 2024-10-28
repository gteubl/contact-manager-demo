using System.ComponentModel.DataAnnotations;
using ContactManagerDemo.Domain.Enums;

namespace ContactManagerDemo.Domain.Entities;

public class Contact : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    public Gender Gender { get; set; }

    [Required]
    [MaxLength(254)]
    public string Email { get; set; }

    public DateTime? BirthDate { get; set; }

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    public City? City { get; set; }
    public Guid? CityId { get; set; }
}