namespace ATMSystem.Core.ApplicationServices.OperationResults;

public abstract class AccountOperationResult : ResultType
{
    protected AccountOperationResult(bool isSuccess, string? result = null)
        : base(isSuccess, result) { }

    public sealed class AuthenticateFaild : AccountOperationResult
    {
        public AuthenticateFaild(string? message) : base(false, message) { }
    }

    public sealed class AuthenticateFaild<T> : ResultTypeValue<T>
    {
        public AuthenticateFaild(string? message) : base(false, default, message) { }
    }

    public sealed class AccountNotExist : AccountOperationResult
    {
        public AccountNotExist() : base(false, "Account does not exist") { }
    }

    public sealed class AccountNotExist<T> : ResultTypeValue<T>
    {
        public AccountNotExist() : base(false, default, "Account does not exist") { }
    }

    public sealed class InsufficientFunds : AccountOperationResult
    {
        public InsufficientFunds() : base(false, "Insufficient funds") { }
    }

    public sealed class AccountHasMoney : AccountOperationResult
    {
        public AccountHasMoney() : base(false, "Account has money") { }
    }

    public sealed class InValidMoneyValue : AccountOperationResult
    {
        public InValidMoneyValue() : base(false, "Invalid money value") { }
    }
}