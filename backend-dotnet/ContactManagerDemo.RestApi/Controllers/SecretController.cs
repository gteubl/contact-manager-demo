using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerDemo.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class SecretController : ControllerBase
{
    
    [HttpGet("secret")]
    public ActionResult<string> GetSecret() => Ok("Questo è un segreto");
    
    [HttpGet("non-secret")]
    [AllowAnonymous]
    public ActionResult<string> GetNonSecret() => Ok("Questo non è un segreto");
    
}