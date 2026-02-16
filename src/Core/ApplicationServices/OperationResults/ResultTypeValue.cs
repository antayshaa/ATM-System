namespace ATMSystem.Core.ApplicationServices.OperationResults;

public abstract class ResultTypeValue<T> : ResultType
{
    public T? Value { get; }

    protected ResultTypeValue(bool isSuccess, T? value, string? error) : base(isSuccess, error)
    {
        Value = value;
    }

    public sealed class SuccessResult : ResultTypeValue<T>
    {
        public SuccessResult(T value) : base(true, value, "Success") { }
    }
}