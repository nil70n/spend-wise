using System.Globalization;
using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace SpendWise.Core.AccountAggregate;

public class Account(string userKey, string name, CultureInfo culture, DateTime createdAt) : EntityBase<Guid>, IAggregateRoot
{
    public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameof(name));
    public string UserKey { get; private set; } = Guard.Against.NullOrEmpty(userKey, nameof(userKey));
    public CultureInfo Culture { get; private set; } = Guard.Against.Null(culture, nameof(culture));
    public DateTime CreatedAt { get; private set; } = Guard.Against.Null(createdAt, nameof(createdAt));
    public int Balance { get; private set; }

    public void UpdateBalance(int newBalance)
    {
        Balance = newBalance;
    }
}
