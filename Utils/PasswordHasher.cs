using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace expenses_api.Utils;

public class PasswordHasher
{
    public static string HashPassword(string password)
    {
        // Generate a 128-bit salt using a secure PRNG
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Derive a 256-bit subkey (use HMACSHA256 with 10,000 iterations)
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        // Combine the salt and hashed password for storage
        return $"{Convert.ToBase64String(salt)}:{hashed}";
    }

    public static bool VerifyPassword(string hashedPasswordWithSalt, string passwordToCheck)
    {
        // Extract the salt and hashed password from the stored value
        var parts = hashedPasswordWithSalt.Split(':');
        if (parts.Length != 2)
        {
            throw new FormatException("Unexpected format for stored password hash.");
        }

        var salt = Convert.FromBase64String(parts[0]);
        var storedHash = parts[1];

        // Hash the incoming password using the stored salt
        string incomingHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: passwordToCheck,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        // Compare the stored hash with the incoming hash
        return storedHash == incomingHash;
    }
}