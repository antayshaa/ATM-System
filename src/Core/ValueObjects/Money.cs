namespace ATMSystem.Core.ValueObjects;

public class Money
{
    public Money(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Value can't be negative", nameof(value));
        }

        Value = value;
    }

    public decimal Value { get; }
}