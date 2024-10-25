using ContactManagerDemo.Application.Services;

namespace ContactManagerDemo.Tests;

public class CityServiceTests : TestBase
{
    [Test]
    public void GetCitiesAsyncTest()
    {
        
        
        // Arrange
        var cityService = new CityService(AppDataContext, Mapper);
        
        // Act
        var result = cityService.GetCitiesAsync().GetAwaiter().GetResult();
        
        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
    }
    
    [Test]
    public void GetCityByIdAsyncTest()
    {
        
        // Arrange
        var cityService = new CityService(AppDataContext, Mapper);
        var cityId = Guid.Parse("59FE6A19-1672-4D63-885E-02D167E42648");
        
        // Act
        var result = cityService.GetCityByIdAsync(cityId).GetAwaiter().GetResult();
        
        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Name == "Bologna" && result.Province == "BO");
    }
    
}