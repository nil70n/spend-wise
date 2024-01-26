using System.Runtime.CompilerServices;
using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using SpendWise.Core.AccountAggregate;

namespace SpendWise.Core.TransactionAggregate;

public class Transaction(Account account,
                         int value,
                         TransactionType type,
                         DateTime createdAt,
                         [CallerArgumentExpression("account")] string accountParameter = "",
                         [CallerArgumentExpression("value")] string valueParameter = "",
                         [CallerArgumentExpression("createdAt")] string createdAtParameter = "") : EntityBase<Guid>, IAggregateRoot
{
    public Account Account { get; private set; } = Guard.Against.Null(account, accountParameter);
    public int Value { get; private set; } = Guard.Against.Zero(value, valueParameter);
    public TransactionType Type { get; private set; } = type;
    public TransactionState State { get; private set; } = TransactionState.New;
    public DateTime CreatedAt { get; private set; } = Guard.Against.Null(createdAt, createdAtParameter);

    public void NextState()
    {
        State = TransactionState.Calculated;
    }
}

