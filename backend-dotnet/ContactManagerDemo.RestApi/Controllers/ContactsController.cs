using ContactManagerDemo.Application.Commands.Contacts;
using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Application.Queries.Contacts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController  : ControllerBase
{
    private readonly IMediator _mediator;

    public ContactsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetContacts()
    {
        var query = new GetContactsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact(ContactDto contactDto)
    {
        var command = new CreateContactCommand(contactDto);
        var result = await _mediator.Send(command);
        return Ok(result);
    
}
}