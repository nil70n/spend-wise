using Ardalis.SmartEnum;

namespace SpendWise.Core.Logging.Scopes;

public class ApplicationScope(string name, int value)
  : SmartEnum<ApplicationScope>(name, value)
{
    // Core
    public static readonly ApplicationScope CoreServices = new(nameof(CoreServices), LoggingEventId.GetChildEventId(Project.Core.Value, 1));
    public static readonly ApplicationScope CoreTransaction = new(nameof(CoreTransaction), LoggingEventId.GetChildEventId(Project.Core.Value, 2));

    // Infrastructure
    public static readonly ApplicationScope InfrastructureData = new(nameof(InfrastructureData), LoggingEventId.GetChildEventId(Project.Infrastructure.Value, 1));

    // UseCases
    public static readonly ApplicationScope UseCasesTransaction = new(nameof(UseCasesTransaction), LoggingEventId.GetChildEventId(Project.UseCases.Value, 1));
    public static readonly ApplicationScope UseCasesAccount = new(nameof(UseCasesAccount), LoggingEventId.GetChildEventId(Project.UseCases.Value, 2));

    // Web
    public static readonly ApplicationScope WebTransaction = new(nameof(WebTransaction), LoggingEventId.GetChildEventId(Project.Web.Value, 1));
}
