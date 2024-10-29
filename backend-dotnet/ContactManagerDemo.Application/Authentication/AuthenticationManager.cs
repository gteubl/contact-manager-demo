using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Application.Services;
using ContactManagerDemo.Domain.Entities;
using ContactManagerDemo.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ContactManagerDemo.Application.Authentication;

public interface IAuthenticationManager
{
    Task<AuthUserDto?> Authenticate(string username, string password);
    Task<AuthUserDto?> RefreshToken(string token, string refreshToken);
}

public static class CustomClaimTypes
{
    public static string UserId => "Id";
}


public class AuthenticationManager : IAuthenticationManager
{
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _tokenKey;

    public AuthenticationManager(string tokenKey, IConfiguration configuration, IServiceProvider serviceProvider)
    {
        _tokenKey = tokenKey;
        _configuration = configuration;
        _serviceProvider = serviceProvider;
    }


    public async Task<AuthUserDto?> Authenticate(string username, string password)
    {
        var connectionString = _configuration.GetConnectionString("SqlDataConnection");
        var optionsBuilder = new DbContextOptionsBuilder<AppDataContext>();
        optionsBuilder.UseSqlServer(connectionString);
        var options = optionsBuilder.Options;
        await using var context = new AppDataContext(options);
        
        var loginUser = await context.Users.IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Username == username);

        if (loginUser == null)
        {
            return null;
        }
        var passwordService = new PasswordService();
        var isPasswordValid = passwordService.ValidatePassword(password, loginUser.Salt, loginUser.HashedPassword);

        if (!isPasswordValid)
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenKey);

        var tokenDescriptor = GetTokenDescription(loginUser, key);

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);

        var refreshToken = GenerateRefreshToken();
        loginUser.RefreshToken = refreshToken;
        loginUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(12);

        context.Users.Update(loginUser);
        await context.SaveChangesAsync();

        return GetAuthUser(accessToken, tokenDescriptor, loginUser, refreshToken);
    }

    public async Task<AuthUserDto?> RefreshToken(string token, string refreshToken)
    {
        var principal = GetPrincipalFromExpiredToken(token);
        var userId = AuthUtils.GetCurrentUserId(principal);

        var connectionString = _configuration.GetConnectionString("SqlDataConnection");
        var optionsBuilder = new DbContextOptionsBuilder<AppDataContext>();
        optionsBuilder.UseSqlServer(connectionString);
        var options = optionsBuilder.Options;
        await using var context = new AppDataContext(options);
        
        var user = await context.Users.IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Id == userId && u.RefreshToken == refreshToken);

        if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return null;
        }

        var newAccessToken = GenerateAccessToken(user);

        /* Endless refresh:  uncomment this block to enable endless refresh
        * var newRefreshToken = GenerateRefreshToken();
        * user.RefreshToken = newRefreshToken;
        * _context.Users.Update(user);
        * await ctx.SaveChangesAsync();
        */

        return new AuthUserDto
        {
            AccessToken = newAccessToken,
            RefreshToken = refreshToken, //newRefreshToken,
            ExpiresIn = DateTime.UtcNow.AddHours(1),
            UserId = user.Id,
            Username = user.Username,
        };
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        //Check Program.cs  as well
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenKey)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    private string GenerateAccessToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenKey);

        var tokenDescriptor = GetTokenDescription(user, key);
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }


    private SecurityTokenDescriptor GetTokenDescription(User loginUser, byte[] key)
    {
        return new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(GetClaims(loginUser)),
            Issuer = _configuration.GetValue<string>("Token:Issuer"),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
    }

    private static IEnumerable<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username),
        };
        
        return claims;
    }

    private static AuthUserDto GetAuthUser(string accessToken, SecurityTokenDescriptor tokenDescriptor,
        User user, string refreshToken)
    {
        var authUser = new AuthUserDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = tokenDescriptor!.Expires!.Value,
            UserId = user.Id,
            Username = user.Username,
        };
        return authUser;
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}