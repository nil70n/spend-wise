using Ardalis.SmartEnum;

namespace SpendWise.Core.TransactionAggregate;

public class TransactionState(string name, int value) : SmartEnum<TransactionState>(name, value)
{
    public static readonly TransactionState New = new(nameof(New), 1);
    public static readonly TransactionState Calculated = new(nameof(Calculated), 2);
}
