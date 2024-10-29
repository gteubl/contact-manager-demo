using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Application.Queries.Cities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CitiesController(IMediator mediator) => _mediator = mediator;
    
    
    [HttpGet("cities")]
    public async Task<ActionResult<List<CityDto>>> GetCities()
    {
        var query = new GetCitiesQuery();
        var result = await _mediator.Send(query, HttpContext.RequestAborted);
        return Ok(result);
    }
}