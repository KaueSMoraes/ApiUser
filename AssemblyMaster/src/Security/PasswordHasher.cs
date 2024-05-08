using System.Security.Cryptography;
using System.Text;

namespace AssemblyMaster.Security;

public static class PasswordHasher
{
    public static string HashPassword(string password, string salt)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password + salt);
        var hash = SHA256.Create().ComputeHash(passwordBytes);
        return Convert.ToBase64String(hash);
    }

    public static string GenerateSalt()
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
    }
}

