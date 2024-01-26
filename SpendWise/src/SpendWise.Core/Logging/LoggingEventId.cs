using Microsoft.Extensions.Logging;
using SpendWise.Core.Logging.Scopes;

namespace SpendWise.Core.Logging;

public static class LoggingEventId
{
    internal static int GetChildEventId(int scopeId, int id)
    {
        return (scopeId * 100) + id;
    }

    public static EventId Get(ApplicationScope module, int id, string name)
    {
        return new EventId(GetChildEventId(module.Value, id), name);
    }
}
