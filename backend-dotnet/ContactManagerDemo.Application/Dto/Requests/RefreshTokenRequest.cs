namespace ContactManagerDemo.Application.Dto.Requests
{
    public class RefreshTokenRequest
    {
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
    }
}