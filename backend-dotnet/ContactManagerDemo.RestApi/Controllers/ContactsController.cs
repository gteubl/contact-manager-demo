using ContactManagerDemo.Application.Commands.Contacts;
using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Application.Dto.Requests;
using ContactManagerDemo.Application.Dto.Responses;
using ContactManagerDemo.Application.Queries.Contacts;
using ContactManagerDemo.Common.GridData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContactsController(IMediator mediator) => _mediator = mediator;

    /*  In questa richiesta utilizziamo il metodo POST principalmente a causa del parametro `ColumnsToFilter`,
     che richiede una struttura dati complessa e non si presta facilmente a una query string in GET.
     Mentre parametri come `Skip`, `Take`, `OrderBy` e `OrderDescending` avrebbero potuto essere gestiti
      tramite GET, l'uso di POST permette una maggiore flessibilità per inviare strutture di dati più articolate. */
    [HttpPost ("contacts")]
    public async  Task<ActionResult<GridDataSource<ContactsResponse>>> GetContacts(ContactsRequest request)
    {
        var query = new GetContactsQuery(request);
        var result = await _mediator.Send(query, HttpContext.RequestAborted);
        return Ok(result);
    }
    
    /* In questa richiesta utilizziamo il metodo standard GET poiché i parametri sono semplici e possono essere facilmente passati come query string.*/
    [HttpGet("contacts")]
    public async Task<ActionResult<GridDataSource<ContactsResponse>>> GetSimpleContacts([FromQuery] int skip = 0, [FromQuery] int take = 10, [FromQuery] string? orderBy = null, [FromQuery] bool orderDescending = false,
        [FromQuery] string? magicFilter = null
        )
    {
        var request = new GridDataRequest
        {
            Skip = skip,
            Take = take,
            OrderBy = orderBy,
            OrderDescending = orderDescending,
            ColumnsToFilter = new List<string>(), // Ignora ColumnsToFilter
            MagicFilter = magicFilter
        };

        var query = new GetContactsQuery(request);
        var result = await _mediator.Send(query, HttpContext.RequestAborted);
        return Ok(result);
    }

    [HttpPost("create-contacts")]
    public async Task<IActionResult> CreateContact(ContactDto contactDto)
    {
        var command = new CreateContactCommand(contactDto);
        var result = await _mediator.Send(command, HttpContext.RequestAborted);
        return Ok(result);
    }
}