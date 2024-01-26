using Microsoft.Extensions.Logging;
using SpendWise.Core.Logging;
using SpendWise.Core.Logging.Scopes;

namespace SpendWise.Core.Services.LoggerDefinitions;

internal static class EventIds
{
    internal static readonly EventId UpdateBalanceStartedId
      = LoggingEventId.Get(ApplicationScope.CoreServices, 1, nameof(ILoggerExtensions.UpdateBalanceStarted));

    internal static readonly EventId UpdateBalanceAccountNotFoundId
      = LoggingEventId.Get(ApplicationScope.CoreServices, 2, nameof(ILoggerExtensions.UpdateBalanceAccountNotFound));

    internal static readonly EventId UpdateBalanceAcccountErrorId
      = LoggingEventId.Get(ApplicationScope.CoreServices, 3, nameof(ILoggerExtensions.UpdateBalanceAcccountError));

    internal static readonly EventId UpdateBalanceAccountSuccessId
      = LoggingEventId.Get(ApplicationScope.CoreServices, 4, nameof(ILoggerExtensions.UpdateBalanceAccountSuccess));
}

