using System.Security.Cryptography;
using System.Text;

namespace ContactManagerDemo.Application.Services;

public interface IPasswordService
{
    string  GenerateSalt(int size = 16);
    bool ValidatePassword(string password, string base64Salt, string hash);
    string HashPassword(string password, string base64Salt);
}

public class PasswordService : IPasswordService
{
    public string GenerateSalt(int size = 16)
    {
        var salt = new byte[size];
        RandomNumberGenerator.Fill(salt);
        return Convert.ToBase64String(salt);
    }

    private static byte[] Base64ToSalt(string base64Salt)
    {
        return Convert.FromBase64String(base64Salt);
    }

    public string HashPassword(string password, string base64Salt)
    {
        var salt = Base64ToSalt(base64Salt);
        using var hmac = new HMACSHA256(salt);
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = hmac.ComputeHash(passwordBytes);
        return Convert.ToBase64String(hashBytes);
    }

    public  bool ValidatePassword(string password, string base64Salt, string hash)
    {
        try
        {
            var computedHash = HashPassword(password, base64Salt);
            return hash == computedHash;
        }
        catch (Exception e)
        {
            return false;
        }
       
    }
}