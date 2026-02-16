using ATMSystem.Core.ApplicationServices.Abstractions;
using ATMSystem.Core.Configuration;
using Microsoft.Extensions.Options;

namespace ATMSystem.Infrastructure.InMemoryDataAccess;

public class InMemorySystemPassword : ISystemPassword
{
    private readonly string _systemPassword;

    public InMemorySystemPassword(IOptions<AdminSettings> options)
    {
        _systemPassword = options.Value.SystemPassword;
    }

    public Task<bool> IsValidPassword(string password)
    {
        return Task.FromResult(password == _systemPassword);
    }
}