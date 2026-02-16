using DbUp;
using System.Reflection;

namespace ATMSystem.Infrastructure.DataBaseDataAccess;

public static class DatabaseMigrate
{
    public static bool MigrateDatabase(string connection)
    {
        EnsureDatabase.For.PostgresqlDatabase(connection);

        DbUp.Engine.UpgradeEngine upgrader = DeployChanges.To
            .PostgresqlDatabase(connection)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        DbUp.Engine.DatabaseUpgradeResult result = upgrader.PerformUpgrade();

        return result.Successful;
    }
}