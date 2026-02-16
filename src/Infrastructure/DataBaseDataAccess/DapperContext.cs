using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace ATMSystem.Infrastructure.DataBaseDataAccess;

public class DapperContext
{
    private readonly string _connectionString;

    public DapperContext(IOptions<DatabaseSettings> options)
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        _connectionString = options.Value.Connection;
    }

    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}