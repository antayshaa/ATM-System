using ATMSystem.Core.ApplicationServices.Abstractions;
using ATMSystem.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ATMSystem.Infrastructure.DataBaseDataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgresAccessToData(this IServiceCollection services, string connection)
    {
        services.AddSingleton<IAccountRepository, PostgresAccountRepository>();
        services.AddSingleton<IOperationRepository, PostgresOperationRepository>();
        services.AddSingleton<ISessionRepository, PostgresSessionRepository>();
        services.AddSingleton<ISystemPassword, PostgresSystemPassword>();
        services.AddSingleton<DapperContext>();
        services.Configure<DatabaseSettings>(x => x.Connection = connection);

        return services;
    }
}