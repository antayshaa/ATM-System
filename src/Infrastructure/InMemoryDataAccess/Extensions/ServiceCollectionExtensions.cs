using ATMSystem.Core.ApplicationServices.Abstractions;
using ATMSystem.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ATMSystem.Infrastructure.InMemoryDataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInMemoryDataAccess(this IServiceCollection services)
    {
        services.AddSingleton<IAccountRepository, InMemoryAccountRepository>();
        services.AddSingleton<IOperationRepository, InMemoryOperationRepository>();
        services.AddSingleton<ISessionRepository, InMemorySessionRepository>();
        services.AddSingleton<ISystemPassword, InMemorySystemPassword>();

        return services;
    }
}