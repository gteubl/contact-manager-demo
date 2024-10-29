using ContactManagerDemo.Application.Services;
using ContactManagerDemo.Infrastructure.Seeds;

namespace ContactManagerDemo.Tests;

public class PasswordServiceTest : TestBase
{
    private IPasswordService _passwordService;
    
    [SetUp]
    public void Setup()
    {
        _passwordService = new PasswordService();
    }
    
    [Test]
    public void GenerateSalt_ReturnsNonNullString()
    {
        // Act
        var salt = _passwordService.GenerateSalt();
        var hashPassword = _passwordService.HashPassword("admin", salt);
        
        var result = _passwordService.ValidatePassword("admin", salt, hashPassword);
        
        var wrongHash = _passwordService.ValidatePassword("admin", salt, "wrongHash");
        var wrongPassword = _passwordService.ValidatePassword("wrongPassword", salt, hashPassword);

        var anotherSalt = _passwordService.GenerateSalt();
        var wrongSalt = _passwordService.ValidatePassword("admin", anotherSalt, hashPassword);
        
        // Assert
        Assert.IsNotNull(salt);
        Assert.IsNotNull(hashPassword);
        Assert.IsTrue(result);
        Assert.IsFalse(wrongHash);
        Assert.IsFalse(wrongPassword);
        Assert.IsFalse(wrongSalt);
    }
    
    [Test]
    public void TestSeedPassword()
    {
        // Act
        var salt = UserDataSeed.DefaultAdminSalt;
        var hash = UserDataSeed.DefaultAdminHashedPassword;
        
        var result = _passwordService.ValidatePassword("admin", salt, hash);
        
        // Assert
        Assert.IsTrue(result);
    }
    
}