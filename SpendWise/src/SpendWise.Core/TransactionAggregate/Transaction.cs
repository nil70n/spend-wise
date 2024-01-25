using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using SpendWise.Core.AccountAggregate;

namespace SpendWise.Core.TransactionAggregate;

public class Transaction(Account account,
                         int value,
                         TransactionType type,
                         DateTime createdAt) : EntityBase<Guid>, IAggregateRoot
{
    public Account Account { get; private set; } = Guard.Against.Null(account, nameof(account));
    public int Value { get; private set; } = Guard.Against.Zero(value, nameof(value));
    public TransactionType Type { get; private set; } = type;
    public DateTime CreatedAt { get; private set; } = Guard.Against.Null(createdAt, nameof(createdAt));
}

