using System.Globalization;
using System.Runtime.CompilerServices;
using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace SpendWise.Core.AccountAggregate;

public class Account(string userKey,
                     string name,
                     CultureInfo culture,
                     DateTime createdAt,
                     [CallerArgumentExpression("userKey")] string userKeyParameter = "",
                     [CallerArgumentExpression("name")] string nameParameter = "",
                     [CallerArgumentExpression("culture")] string cultureParameter = "",
                     [CallerArgumentExpression("createdAt")] string createdAtParameter = "")
  : EntityBase<Guid>, IAggregateRoot
{
    public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameParameter);
    public string UserKey { get; private set; } = Guard.Against.NullOrEmpty(userKey, userKeyParameter);
    public CultureInfo Culture { get; private set; } = Guard.Against.Null(culture, cultureParameter);
    public DateTime CreatedAt { get; private set; } = Guard.Against.Null(createdAt, createdAtParameter);
    public int Balance { get; private set; }

    public void UpdateBalance(int newBalance)
    {
        Balance = newBalance;
    }
}
