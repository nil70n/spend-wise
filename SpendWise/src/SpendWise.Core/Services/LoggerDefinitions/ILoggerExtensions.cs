using Microsoft.Extensions.Logging;

namespace SpendWise.Core.Services.LoggerDefinitions;

internal static class ILoggerExtensions
{
    internal static void UpdateBalanceStarted(
        this ILogger logger, Guid transactionId, DateTime startedAt)
    {
        LoggingActions.UpdateBalanceStartedAction(
            logger, transactionId, startedAt, default!);
    }

    internal static void UpdateBalanceAccountNotFound(
        this ILogger logger, Guid accountId, DateTime failedAt)
    {
        LoggingActions.UpdateBalanceAccountNotFoundAction(
            logger, accountId, failedAt, default!);
    }

    internal static void UpdateBalanceAcccountError(
        this ILogger logger, Guid transactionId, DateTime failedAt, Exception exception)
    {
        LoggingActions.UpdateBalanceAcccountErrorAction(
            logger, transactionId, failedAt, exception);
    }

    internal static void UpdateBalanceAccountSuccess(
        this ILogger logger, Guid transactionId, DateTime finishedAt)
    {
        LoggingActions.UpdateBalanceAccountSuccessAction(
            logger, transactionId, finishedAt, default!);
    }
}

