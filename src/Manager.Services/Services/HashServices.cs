using System.Text;
using Konscious.Security.Cryptography;

namespace Manager.Services.Services;

public class HashServices
{
    public string GenerateHash(string password)
    {
        byte[] salt = new byte[32];

        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        var argon2 = new Argon2i(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = 8,
            Iterations = 4,
            MemorySize = 1024 * 1024
        };

        byte[] hash = argon2.GetBytes(32);

        byte[] saltedHash = new byte[salt.Length + hash.Length];
        Array.Copy(salt, 0, saltedHash, 0, salt.Length);
        Array.Copy(hash, 0, saltedHash, salt.Length, hash.Length);

        return Convert.ToBase64String(saltedHash);
    }

    public bool VerifyPassword(string password, string hash)
    {
        byte[] saltedHash = Convert.FromBase64String(hash);

        byte[] salt = new byte[32];
        Array.Copy(saltedHash, 0, salt, 0, salt.Length);

        var argon2 = new Argon2i(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = 8,
            Iterations = 4,
            MemorySize = 1024 * 1024
        };

        byte[] newHash = argon2.GetBytes(32);
        
        byte[] saltedNewHash = new byte[salt.Length + newHash.Length];
        Array.Copy(salt, 0, saltedNewHash, 0, salt.Length);
        Array.Copy(newHash, 0, saltedNewHash, salt.Length, newHash.Length);

        return saltedNewHash.SequenceEqual(saltedHash);
    }
}
