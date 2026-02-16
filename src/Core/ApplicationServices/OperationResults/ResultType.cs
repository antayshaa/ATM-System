namespace ATMSystem.Core.ApplicationServices.OperationResults;

public abstract class ResultType
{
    public bool IsSuccess { get; }

    public string? Result { get; }

    protected ResultType(bool isSuccess, string? result = null)
    {
        IsSuccess = isSuccess;
        Result = result;
    }

    public sealed class Success : UserSessionResult
    {
        public Success() : base(true, "Success") { }
    }
}