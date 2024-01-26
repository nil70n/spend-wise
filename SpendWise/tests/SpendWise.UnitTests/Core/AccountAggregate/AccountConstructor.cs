using System.Globalization;
using SpendWise.Core.AccountAggregate;
using Xunit;

namespace SpendWise.UnitTests.Core.AccountAggregate;

public class AccountConstructor
{
    private readonly string _userKey = "user";
    private readonly string _accountName = "AccountName";
    private readonly CultureInfo _culture = new("en-US");

    private Account CreateAccount(DateTime createdAt)
    {
        return new Account(_userKey,
                           _accountName,
                           _culture,
                           createdAt);
    }

    [Fact]
    public void InitializesAccount()
    {
        var now = DateTime.UtcNow;
        var account = CreateAccount(now);

        Assert.Equal(_userKey, account.UserKey);
        Assert.Equal(_accountName, account.Name);
        Assert.Equal(_culture, account.Culture);
        Assert.Equal(now, account.CreatedAt);
    }
}
