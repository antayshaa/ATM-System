using ATMSystem.Core.DomainModel;
using ATMSystem.Core.Repositories;
using ATMSystem.Infrastructure.DataBaseDataAccess.DTO;
using Dapper;

namespace ATMSystem.Infrastructure.DataBaseDataAccess;

public class PostgresAccountRepository : IAccountRepository
{
    private readonly DapperContext _dapperContext;

    public PostgresAccountRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<Account?> FindAccountById(Guid accountId)
    {
        const string query = """
                        SELECT id, balance, password_hash as password
                        FROM accounts 
                        WHERE id = @AccountId;
                        """;

        var parameters = new DynamicParameters();
        parameters.Add("AccountId", accountId);

        using System.Data.IDbConnection connection = _dapperContext.CreateConnection();
        AccountDbModel? dbModel = await connection.QueryFirstOrDefaultAsync<AccountDbModel>(query, parameters);
        if (dbModel is null)
        {
            return null;
        }

        return new Account(dbModel.Id, dbModel.Balance, dbModel.Password);
    }

    public async Task UpdateAccount(Account account)
    {
        const string query = """
                        UPDATE accounts 
                        SET balance = @Balance, 
                            password_hash = @Password
                        WHERE id = @Id;
                        """;

        var parameters = new DynamicParameters();
        parameters.Add("Id", account.Id);
        parameters.Add("Balance", account.Balance.Value);
        parameters.Add("Password", account.Password);

        using System.Data.IDbConnection connection = _dapperContext.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }

    public async Task AddAccount(Account account)
    {
        const string query = """
                        INSERT INTO accounts (id, balance, password_hash, created_at)
                        VALUES (@Id, @Balance, @Password, @CreatedAt);
                        """;

        var parameters = new DynamicParameters();
        parameters.Add("Id", account.Id);
        parameters.Add("Balance", account.Balance.Value);
        parameters.Add("Password", account.Password);
        parameters.Add("CreatedAt", DateTime.Now);

        using System.Data.IDbConnection connection = _dapperContext.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }

    public async Task DeleteAccount(Guid accountId)
    {
        const string query = """
                        DELETE FROM accounts WHERE id = @AccountId
                        """;

        var parameters = new DynamicParameters();
        parameters.Add("AccountId", accountId);

        using System.Data.IDbConnection connection = _dapperContext.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }
}