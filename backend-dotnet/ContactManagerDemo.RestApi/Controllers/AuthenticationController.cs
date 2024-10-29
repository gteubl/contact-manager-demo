using ContactManagerDemo.Application.Authentication;
using ContactManagerDemo.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RefreshTokenRequest = ContactManagerDemo.Application.Dto.Requests.RefreshTokenRequest;

namespace ContactManagerDemo.Controllers;

[Route("api/auth")]
public class AuthorizationController : ControllerBase
{
    private readonly IAuthenticationManager _authenticationManager;

    public AuthorizationController(IAuthenticationManager authenticationManager)
    {
        _authenticationManager = authenticationManager;
    }

    [HttpPost("token")]
    [AllowAnonymous]
    public async Task<IActionResult> Token(string username, string password)
    {
        var token = await _authenticationManager.Authenticate(username, password);
        if (token == null)
        {
            return Unauthorized("Utenza o password non valide");
        }

        return Ok(token);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        var authUser = await _authenticationManager.RefreshToken(request.Token, request.RefreshToken);
        if (authUser != null)
        {
            return Ok(authUser);
        }

        HttpContext.Response.Headers.Remove("Token-Expired");
        return Unauthorized("Rifare il login");

    }
    
    [HttpGet("test")]
    [Authorize]
    public IActionResult TestToken() => new OkResult();

}