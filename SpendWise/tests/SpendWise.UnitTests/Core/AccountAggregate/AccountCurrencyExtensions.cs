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
    [InlineData("en-US", 1, 0.01f)]
    [InlineData("en-US", 100, 1f)]
    [InlineData("en-US", 199, 1.99f)]
    [InlineData("en-US", 1000, 10f)]
    [InlineData("en-US", 1990, 19.9f)]
    [InlineData("en-US", 120055, 1200.55f)]
    [InlineData("ja-JP", 1, 1f)]
    [InlineData("ja-JP", 100, 100f)]
    [InlineData("ja-JP", 199, 199f)]
    [InlineData("ja-JP", 1000, 1000f)]
    [InlineData("ja-JP", 1990, 1990f)]
    [InlineData("ja-JP", 120055, 120055f)]
    [InlineData("ar-BH", 1, 0.001f)]
    [InlineData("ar-BH", 100, 0.1f)]
    [InlineData("ar-BH", 199, 0.199f)]
    [InlineData("ar-BH", 1000, 1f)]
    [InlineData("ar-BH", 1990, 1.99f)]
    [InlineData("ar-BH", 120055, 120.055f)]
    public void GetFloatValue(string cultureName, int input, float expected)
    {
        var account = CreateAccount(new CultureInfo(cultureName));

        Assert.Equal(expected, account.GetFloatValue(input));
    }

    [Theory]
    [InlineData("en-US", 0.01f, 1)]
    [InlineData("en-US", 1f, 100)]
    [InlineData("en-US", 1.99f, 199)]
    [InlineData("en-US", 10f, 1000)]
    [InlineData("en-US", 19.9f, 1990)]
    [InlineData("en-US", 1200.55f, 120055)]
    [InlineData("ja-JP", 1f, 1)]
    [InlineData("ja-JP", 100f, 100)]
    [InlineData("ja-JP", 199f, 199)]
    [InlineData("ja-JP", 1000f, 1000)]
    [InlineData("ja-JP", 1990f, 1990)]
    [InlineData("ja-JP", 1200.55f, 1200)]
    [InlineData("ar-BH", 0.001f, 1)]
    [InlineData("ar-BH", 0.1f, 100)]
    [InlineData("ar-BH", 0.199f, 199)]
    [InlineData("ar-BH", 1f, 1000)]
    [InlineData("ar-BH", 1.99f, 1990)]
    [InlineData("ar-BH", 120.055f, 120055)]
    public void GetIntegerValue(string cultureName, float input, int expected)
    {
        var account = CreateAccount(new CultureInfo(cultureName));

        Assert.Equal(expected, account.GetIntegerValue(input));
    }

    [Theory]
    [InlineData("en-US", 1, "$0.01")]
    [InlineData("en-US", 100, "$1.00")]
    [InlineData("en-US", 199, "$1.99")]
    [InlineData("en-US", 1000, "$10.00")]
    [InlineData("en-US", 1990, "$19.90")]
    [InlineData("en-US", 120055, "$1,200.55")]
    [InlineData("ja-JP", 1, "￥1")]
    [InlineData("ja-JP", 100, "￥100")]
    [InlineData("ja-JP", 199, "￥199")]
    [InlineData("ja-JP", 1000, "￥1,000")]
    [InlineData("ja-JP", 1990, "￥1,990")]
    [InlineData("ja-JP", 120055, "￥120,055")]
    [InlineData("ar-BH", 1, "0٫001 د.ب.‏")]
    [InlineData("ar-BH", 100, "0٫100 د.ب.‏")]
    [InlineData("ar-BH", 199, "0٫199 د.ب.‏")]
    [InlineData("ar-BH", 1000, "1٫000 د.ب.‏")]
    [InlineData("ar-BH", 1990, "1٫990 د.ب.‏")]
    [InlineData("ar-BH", 120055, "120٫055 د.ب.‏")]
    public void GetDisplayValue(string cultureName, int input, string expected)
    {
        var account = CreateAccount(new CultureInfo(cultureName));

        Assert.Equal(expected, account.GetDisplayValue(input));
    }
}
