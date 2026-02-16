using ATMSystem.Core.ApplicationServices.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace ATMSystem.Core.ApplicationServices.Services;

public class PasswordService : IPasswordService
{
    public string GetHash(string password)
    {
        return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
    }

    public bool VerifyPassword(string password, string hash)
    {
        byte[] currentHash = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        byte[] expected = Convert.FromBase64String(hash);
        return currentHash.SequenceEqual(expected);
    }
}