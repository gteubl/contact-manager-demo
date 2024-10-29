using System.Text.Json.Serialization;

namespace ContactManagerDemo.Application.Dto;

public class AuthUserDto
{
    public Guid UserId { get; set; }
    
    public string Username { get; set; }
    
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("expires_in")]
    public DateTime ExpiresIn { get; set; }
        
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get;  set; }
}