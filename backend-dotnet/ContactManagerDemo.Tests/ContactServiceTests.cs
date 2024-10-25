using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Application.Services;
using ContactManagerDemo.Domain.Enums;

namespace ContactManagerDemo.Tests;

public class ContactServiceTests : TestBase
{
    [Test]
    public void GetContactsAsyncTest()
    {
        // Arrange
        var contactService = new ContactService(AppDataContext, Mapper);

        // Act
        var result = contactService.GetContactsAsync().GetAwaiter().GetResult();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
    }

    [Test]
    public async Task GetContactByIdAsyncTest()
    {
        // Arrange
        var contactService = new ContactService(AppDataContext, Mapper);
        var existingContactId = Guid.Parse("1773734c-5d93-48d4-8c96-6ad7e1f16d7c");

        // Act
        var result = await contactService.GetContactByIdAsync(existingContactId);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(existingContactId, result.Id);
    }

    [Test]
    public async Task CreateContactAsyncTest()
    {
        // Arrange
        var contactService = new ContactService(AppDataContext, Mapper);
        var newContact = new ContactDto 
        { 
            FirstName = "Joe", 
            LastName = "Doe",
            Email = "joedoe@example.com",
            Gender = Gender.Male,
        };

        // Act
        var result = await contactService.CreateContactAsync(newContact);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(newContact.FirstName, result.FirstName);
        Assert.AreEqual(newContact.LastName, result.LastName);
    }


    [Test]
    public async Task SearchContactsAsyncTest()
    {
        // Arrange
        var contactService = new ContactService(AppDataContext, Mapper);
        var searchTerm = "John";

        // Act
        var results = await contactService.SearchContactsAsync(searchTerm);

        // Assert
        Assert.IsNotNull(results);
        Assert.IsTrue(results.All(c => c.FirstName.Contains(searchTerm) || c.LastName.Contains(searchTerm)));
    }
}