using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ContactManagerDemo.Application.Authentication;

public class AuthUtils
{
    public static Guid? GetUserIdFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token);
        var tokenS = jsonToken as JwtSecurityToken;
        var userId = tokenS?.Claims.First(claim => claim.Type == CustomClaimTypes.UserId).Value;
        
        if (userId == null)
        {
            return null;
        }
        
        return Guid.Parse(userId);
    }

    public static Guid GetCurrentUserId(ClaimsPrincipal user)
    {
        return Guid.Parse(user.Claims.First(e => e.Type == CustomClaimTypes.UserId).Value);
    }
}