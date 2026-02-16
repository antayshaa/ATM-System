using ATMSystem.Core.Configuration;
using ATMSystem.Core.Extensions;
using ATMSystem.Infrastructure.DataBaseDataAccess;
using ATMSystem.Infrastructure.DataBaseDataAccess.Extensions;
using ATMSystem.Infrastructure.InMemoryDataAccess.Extensions;
using Microsoft.OpenApi;

namespace ATMSystem.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Services.Configure<AdminSettings>(builder.Configuration.GetSection("AdminSettings"));

        ConfigureServices(builder.Services);
        ConfigureDataAccess(builder);

        WebApplication app = builder.Build();
        ConfigureApp(app);

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "My API",
                Version = "v1",
                Description = "API for the banking system",
            });
        });
        services.AddControllers();
        services.AddCore();
    }

    private static void ConfigureDataAccess(WebApplicationBuilder builder)
    {
        string? connectionString = builder.Configuration.GetConnectionString("postgres");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            builder.Services.AddInMemoryDataAccess();
        }
        else
        {
            builder.Services.AddPostgresAccessToData(connectionString);
            DatabaseMigrate.MigrateDatabase(connectionString);
        }
    }

    private static void ConfigureApp(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();
    }
}