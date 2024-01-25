using System.Globalization;
using SpendWise.Core.AccountAggregate;
using SpendWise.Core.AccountAggregate.Extensions;
using Xunit;

namespace SpendWise.UnitTests.AccountAggregate;

public class AccountCurrencyExtensions
{
    private readonly string userKey = "user";
    private readonly string accountName = "AccountName";

    private Account CreateAccount(CultureInfo culture)
    {
        return new Account(userKey,
                           accountName,
                           culture,
                           DateTime.UtcNow);
    }

    [Theory]
    [InlineData("en-US", 120055, 1200.55f)]
    [InlineData("ja-JP", 120055, 1200f)]
    [InlineData("ar-BH", 120055, 120.055f)]
    public void GetFloatValue(string cultureName, int input, float expected)
    {
        var account = CreateAccount(new CultureInfo(cultureName));

        Assert.Equal(expected, account.GetFloatValue(input));
    }

    [Theory]
    [InlineData("en-US", 1200.55f, 120055)]
    [InlineData("ja-JP", 1200f, 120055)]
    [InlineData("ar-BH", 120.055f, 120055)]
    public void GetIntegerValue(string cultureName, float input, int expected)
    {
        var account = CreateAccount(new CultureInfo(cultureName));

        Assert.Equal(expected, account.GetIntegerValue(input));
    }

    [Theory]
    [InlineData("en-US", 120055, "$1,200.55")]
    [InlineData("ja-JP", 120055, "¥1,201")]
    [InlineData("ar-BH", 120055, "120.055 .ب.د")]
    public void GetDisplayValue(string cultureName, int input, string expected)
    {
        var account = CreateAccount(new CultureInfo(cultureName));

        Assert.Equal(expected, account.GetDisplayValue(input));
    }
}
