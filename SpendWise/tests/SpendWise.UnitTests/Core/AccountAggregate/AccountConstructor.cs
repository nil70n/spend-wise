using System.Globalization;
using SpendWise.Core.AccountAggregate;
using Xunit;

namespace SpendWise.UnitTests.AccountAggregate;

public class AccountConstructor
{
    private readonly string userKey = "user";
    private readonly string accountName = "AccountName";
    private readonly CultureInfo culture = new CultureInfo("en-US");

    private Account CreateAccount(DateTime createdAt)
    {
        return new Account(userKey,
                           accountName,
                           culture,
                           createdAt);
    }

    [Fact]
    public void InitializesAccount()
    {
        var now = DateTime.UtcNow;
        var account = CreateAccount(now);

        Assert.Equal(userKey, account.UserKey);
        Assert.Equal(accountName, account.Name);
        Assert.Equal(culture, account.Culture);
        Assert.Equal(now, account.CreatedAt);
    }
}
