using ATMSystem.Core.DomainModel;
using ATMSystem.Core.Repositories;
using Dapper;

namespace ATMSystem.Infrastructure.DataBaseDataAccess;

public class PostgresOperationRepository : IOperationRepository
{
    private readonly DapperContext _dapperContext;

    public PostgresOperationRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task AddOperation(Operation operation)
    {
        const string query = """
                             INSERT INTO operations (id, account_id, type, timestamp)
                             VALUES (@Id, @AccountId, @Type, @Timestamp);
                             """;

        var parameters = new DynamicParameters();
        parameters.Add("Id", operation.Id);
        parameters.Add("AccountId", operation.AccountId);
        parameters.Add("Type", operation.Type.ToString());
        parameters.Add("Timestamp", operation.OccurredAt);

        using System.Data.IDbConnection connection = _dapperContext.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }

    public async Task<IReadOnlyCollection<Operation>> GetAccountOperationHistory(Guid accountId)
    {
        const string query = """
                             SELECT id as Id, account_id as AccountId, type as Type, timestamp as OccurredAt
                             FROM operations 
                             WHERE account_id = @AccountId
                             ORDER BY timestamp DESC;
                             """;

        var parameters = new DynamicParameters();
        parameters.Add("AccountId", accountId);

        using System.Data.IDbConnection connection = _dapperContext.CreateConnection();
        IEnumerable<Operation> operations = await connection.QueryAsync<Operation>(query, parameters);

        return operations.ToList().AsReadOnly();
    }

    public async Task DeleteOperation(Guid operationId)
    {
        const string query = """
                             DELETE FROM operations WHERE id = @OperationId
                             """;

        var parameters = new DynamicParameters();
        parameters.Add("OperationId", operationId);

        using System.Data.IDbConnection connection = _dapperContext.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }

    public async Task DeleteOperationsForAccount(Guid accountId)
    {
        const string query = """
                             DELETE FROM operations 
                             WHERE account_id = @AccountId;
                             """;

        var parameters = new DynamicParameters();
        parameters.Add("AccountId", accountId);

        using System.Data.IDbConnection connection = _dapperContext.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }
}