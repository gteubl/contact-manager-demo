using ContactManagerDemo.Application.Queries.Cities;
using ContactManagerDemo.Application.Services;

namespace ContactManagerDemo.Tests;

public class CityServiceTests : TestBase
{
    [Test]
    public async Task GetCitiesAsyncTest()
    {
        
        // Arrange
        // Act
        var result = await Mediator.Send(new GetCitiesQuery());
        
        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
    }
    
    [Test]
    public async Task GetCityByIdAsyncTest()
    {
        
        // Arrange
        var cityId = Guid.Parse("59FE6A19-1672-4D63-885E-02D167E42648");
        
        // Act
        var result = await Mediator.Send(new GetCityByIdQuery(cityId));
        
        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Name == "Bologna" && result.Province == "BO");
    }
    
}