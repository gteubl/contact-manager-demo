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
    
}