using System.Text.Json.Serialization;

namespace ContactManagerDemo.Application.Dto;

public class AuthUser
{
    public Guid UserId { get; set; }
    
    public required string Username { get; set; }
    
    [JsonPropertyName("access_token")]
    public required string AccessToken { get; set; }

    [JsonPropertyName("expires_in")]
    public DateTime ExpiresIn { get; set; }
        
    [JsonPropertyName("refresh_token")]
    public required string RefreshToken { get;  set; }
}