using ATMSystem.Core.ApplicationServices.Abstractions;
using ATMSystem.Core.Configuration;
using Microsoft.Extensions.Options;

namespace ATMSystem.Infrastructure.DataBaseDataAccess;

public class PostgresSystemPassword : ISystemPassword
{
    private readonly string _systemPassword;

    public PostgresSystemPassword(IOptions<AdminSettings> options)
    {
        _systemPassword = options.Value.SystemPassword;
    }

    public Task<bool> IsValidPassword(string password)
    {
        return Task.FromResult(password == _systemPassword);
    }
}