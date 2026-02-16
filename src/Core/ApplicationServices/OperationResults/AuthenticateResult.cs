namespace ATMSystem.Core.ApplicationServices.OperationResults;

public abstract class AuthenticateResult : ResultType
{
    protected AuthenticateResult(bool isSuccess, string? error = null)
        : base(isSuccess, error) { }

    public sealed class SessionNotExist : AuthenticateResult
    {
        public SessionNotExist() : base(false, "Session does not exist") { }
    }

    public sealed class SessionNotExist<T> : ResultTypeValue<T>
    {
        public SessionNotExist() : base(false, default, "Session does not exist") { }
    }

    public sealed class UserIsAdmin<T> : ResultTypeValue<T>
    {
        public UserIsAdmin() : base(false, default, "User is admin") { }
    }

    public sealed class UserIsNotAdmin : AuthenticateResult
    {
        public UserIsNotAdmin() : base(false, "User is not admin") { }
    }

    public sealed class SessionHasNoAccount<T> : ResultTypeValue<T>
    {
        public SessionHasNoAccount() : base(false, default, "Session has no associated account") { }
    }
}