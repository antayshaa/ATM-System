using ATMSystem.Core.ApplicationServices.Abstractions;
using ATMSystem.Core.ApplicationServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ATMSystem.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IAdminSessionService, AdminSessionService>();
        services.AddScoped<IUserSessionService, UserSessionService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddSingleton<IPasswordService, PasswordService>();
        services.AddSingleton<IEventSubscriber, HistoryService>();
        services.AddSingleton<IHistoryService, HistoryService>();
        services.AddSingleton<IEventSubscriber, UserSessionService>();
        services.AddSingleton<IEventPublisher, EventPublisher>();
        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}