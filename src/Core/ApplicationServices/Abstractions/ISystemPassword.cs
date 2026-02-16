namespace ATMSystem.Core.ApplicationServices.Abstractions;

public interface ISystemPassword
{
    Task<bool> IsValidPassword(string password);
}