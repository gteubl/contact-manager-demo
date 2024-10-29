using ContactManagerDemo.Application.Commands.Seed;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly IMediator _mediator;

    public SeedController(IMediator mediator) => _mediator = mediator;
    
    [HttpGet("seed")]
    public async Task<IActionResult> Seed(int qtd = 10)
    {
        var command = new SeedTestContactsCommand(qtd);
        var result = await _mediator.Send(command, HttpContext.RequestAborted);
        return Ok(result);
    }
    
    [HttpDelete("seed")]
    public async Task<IActionResult> RemoveSeed()
    {
        var command = new RemoveTestContactsCommand();
        var result = await _mediator.Send(command, HttpContext.RequestAborted);
        return Ok(result);
    }
    
    
}