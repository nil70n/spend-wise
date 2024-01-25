namespace SpendWise.Core.AccountAggregate.Extensions;

public static class AccountCurrencyExtensions
{
    private static int GetCalculatorValue(Account account)
    {
        return 1;
    }

    public static float GetFloatValue(this Account account, int value)
    {
        return 1;
    }

    public static int GetIntegerValue(this Account account, float value)
    {
        return 1;
    }

    public static string GetDisplayValue(this Account account, int value)
    {
        return "1";
    }
}
