using System.ComponentModel.DataAnnotations;

namespace ContactManagerDemo.Domain.Entities;

public class City : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(2)]
    public string Province { get; set; }
}