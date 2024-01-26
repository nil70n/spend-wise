using System.Globalization;
using SpendWise.Core.AccountAggregate;
using SpendWise.Core.TransactionAggregate;
using Xunit;

namespace SpendWise.UnitTests.Core.TransactionAggregate;

public class TransactionConstructor
{
    private Transaction CreateTransaction(int value,
                                          TransactionType type,
                                          DateTime createdAt)
    {
        var account = new Account("user",
                                  "AccountName",
                                  new CultureInfo("en-US"),
                                  createdAt);

        return new Transaction(account, value, type, createdAt);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(100, 2)]
    [InlineData(1, 1)]
    [InlineData(100, 1)]
    public void InitializesTransaction(int value, int type)
    {
        var now = DateTime.UtcNow;
        var transactionType = TransactionType.FromValue(type);
        var transaction = CreateTransaction(value, transactionType, now);

        Assert.Equal(value, transaction.Value);
        Assert.Equal(transactionType, transaction.Type);
        Assert.Equal(now, transaction.CreatedAt);
        Assert.Equal(TransactionState.New, transaction.State);
    }
}
