using System.ComponentModel.DataAnnotations;

namespace ContactManagerDemo.Domain.Entities;

public class User : BaseEntity
{
    [Required]
    [MaxLength(500)]
    public required string Username { get; set; }
    
    [Required]
    [MaxLength(500)]
    public required string HashedPassword { get; set; }
    
    [Required]
    [MaxLength(500)]
    public required string Salt { get; set; }
    
    [MaxLength(100)]
    public string? RefreshToken { get; set; }
        
    public DateTime? RefreshTokenExpiryTime { get; set; }
    
}