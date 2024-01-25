using Ardalis.SmartEnum;

namespace SpendWise.Core.TransactionAggregate;

public class TransactionType(string name, int value) : SmartEnum<TransactionType>(name, value)
{
    public static readonly TransactionType Debit = new(nameof(Debit), 1);
    public static readonly TransactionType Credit = new(nameof(Credit), 2);
}

