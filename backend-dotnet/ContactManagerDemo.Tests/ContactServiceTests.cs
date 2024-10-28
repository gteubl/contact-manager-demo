using ContactManagerDemo.Application.Commands.Contacts;
using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Application.Dto.Requests;
using ContactManagerDemo.Application.Queries.Contacts;
using ContactManagerDemo.Domain.Enums;

namespace ContactManagerDemo.Tests;

public class ContactServiceTests : TestBase
{
    [Test]
    public async Task GetContactsAsyncTest()
    {
        // Arrange
        var filter = new ContactsRequest()
        {
            Skip = 0,
            Take = 10,
            OrderBy = "FirstName",
            OrderDescending = false,
            ColumnsToFilter = [],
            MagicFilter = null
        };
        
        // Act
        var result = await Mediator.Send(new GetContactsQuery(filter));

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
    }

    [Test]
    public async Task GetContactByIdAsyncTest()
    {
        // Arrange
        var existingContactId = Guid.Parse("1773734c-5d93-48d4-8c96-6ad7e1f16d7c");

        // Act
        var result = await Mediator.Send(new GetContactByIdQuery(existingContactId));

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(existingContactId, result.Id);
    }

    [Test]
    public async Task CreateContactAsyncTest()
    {
        // Arrange
        var newContact = new ContactDto 
        { 
            FirstName = "Joe", 
            LastName = "Doe",
            Email = "joedoe@example.com",
            Gender = Gender.Male,
            
        };

        // Act
        var result = await Mediator.Send(new CreateContactCommand(newContact)); 

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(newContact.FirstName, result.FirstName);
        Assert.AreEqual(newContact.LastName, result.LastName);
    }


    [Test]
    public async Task SearchContactsAsyncTest()
    {
        // Arrange
        var searchTerm = "John";

        // Act
        var results = await Mediator.Send(new SearchContactsQuery(searchTerm));

        // Assert
        Assert.IsNotNull(results);
        Assert.IsTrue(results.All(c => c.FirstName.Contains(searchTerm) || c.LastName.Contains(searchTerm)));
    }
}