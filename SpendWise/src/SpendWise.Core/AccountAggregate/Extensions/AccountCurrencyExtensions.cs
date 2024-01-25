namespace SpendWise.Core.AccountAggregate.Extensions;

public static class AccountCurrencyExtensions
{
    private static int GetCalculatorValue(Account account)
    {
        var decimalCount = account.Culture.NumberFormat.CurrencyDecimalDigits;
        return decimalCount == 0 ? 1 : (int)Math.Pow(10, decimalCount);
    }

    public static float GetFloatValue(this Account account, int value)
    {
        var calculatorValue = GetCalculatorValue(account);
        var integerValue = value / calculatorValue;
        float decimalValue = value % calculatorValue;

        return integerValue + (decimalValue / calculatorValue);
    }

    public static int GetIntegerValue(this Account account, float value)
    {
        return (int)(value * GetCalculatorValue(account));
    }

    public static string GetDisplayValue(this Account account, int value)
    {
        var floatValue = GetFloatValue(account, value);
        return floatValue.ToString("C", account.Culture);
    }
}
