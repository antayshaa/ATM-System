using ATMSystem.Core.DomainModel;
using ATMSystem.Core.Repositories;
using Dapper;

namespace ATMSystem.Infrastructure.DataBaseDataAccess;

public class PostgresSessionRepository : ISessionRepository
{
    private readonly DapperContext _dapperContext;

    public PostgresSessionRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task AddSession(Session session)
    {
        string query = """
                    INSERT INTO sessions (session_key, type, account_id, created_at)
                    VALUES (@SessionKey, @Type, @AccountId, @CreatedAt);
                    """;

        var parameters = new DynamicParameters();
        parameters.Add("SessionKey", session.SessionKey);
        parameters.Add("Type", session.Type.ToString());
        parameters.Add("AccountId", session.AccountId);
        parameters.Add("CreatedAt", DateTime.Now);

        using System.Data.IDbConnection connection = _dapperContext.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }

    public async Task<Session?> FindSessionByKey(Guid sessionKey)
    {
        const string query = """
                        SELECT session_key, type, account_id
                        FROM sessions 
                        WHERE session_key = @SessionKey
                        """;

        var parameters = new DynamicParameters();
        parameters.Add("SessionKey", sessionKey);

        using System.Data.IDbConnection connection = _dapperContext.CreateConnection();
        Session? session = await connection.QueryFirstOrDefaultAsync<Session>(query, parameters);

        return session;
    }

    public async Task DeleteSession(Guid sessionKey)
    {
        const string query = """
                             DELETE FROM sessions WHERE session_key = @SessionKey
                             """;

        var parameters = new DynamicParameters();
        parameters.Add("SessionKey", sessionKey);

        using System.Data.IDbConnection connection = _dapperContext.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }

    public async Task<IReadOnlyCollection<Session>> GetSessionsByAccountId(Guid accountId)
    {
        const string query = """
                             SELECT session_key as SessionKey, type as Type, account_id as AccountId
                             FROM sessions 
                             WHERE account_id = @AccountId
                             """;
        var parameters = new DynamicParameters();
        parameters.Add("AccountId", accountId);

        using System.Data.IDbConnection connection = _dapperContext.CreateConnection();
        IEnumerable<Session> sessions = await connection.QueryAsync<Session>(query, parameters);

        return sessions.ToList().AsReadOnly();
    }
}