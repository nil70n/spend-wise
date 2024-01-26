using Microsoft.Extensions.Logging;

namespace SpendWise.Core.Services.LoggerDefinitions;

internal static class LoggingActions
{
    internal static readonly Action<ILogger, Guid, DateTime, Exception?> UpdateBalanceStartedAction
      = LoggerMessage.Define<Guid, DateTime>(LogLevel.Information,
                                                    EventIds.UpdateBalanceStartedId,
                                                    "Balance Update started at {DateTime}. {TransactionId}");

    internal static readonly Action<ILogger, Guid, DateTime, Exception?> UpdateBalanceAccountNotFoundAction
      = LoggerMessage.Define<Guid, DateTime>(LogLevel.Error,
                                             EventIds.UpdateBalanceAccountNotFoundId,
                                             "Balance Update failed at {DateTime}. Account {AccountId} not found");

    internal static readonly Action<ILogger, Guid, DateTime, Exception> UpdateBalanceAcccountErrorAction
      = LoggerMessage.Define<Guid, DateTime>(LogLevel.Critical,
                                             EventIds.UpdateBalanceAcccountErrorId,
                                             "Balance Update failed at {DateTime}. Error when saving Transaction {TransactionId}");

    internal static readonly Action<ILogger, Guid, DateTime, Exception?> UpdateBalanceAccountSuccessAction
      = LoggerMessage.Define<Guid, DateTime>(LogLevel.Information,
                                             EventIds.UpdateBalanceAccountSuccessId,
                                             "Balance Update for Transaction {TransactionId} finished successfully at {DateTime}.");
}

