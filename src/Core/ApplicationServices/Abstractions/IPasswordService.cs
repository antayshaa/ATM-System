namespace ATMSystem.Core.ApplicationServices.Abstractions;

public interface IPasswordService
{
    string GetHash(string password);

    bool VerifyPassword(string password, string hash);
}