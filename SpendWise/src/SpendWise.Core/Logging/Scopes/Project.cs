using Ardalis.SmartEnum;

namespace SpendWise.Core.Logging.Scopes;

public class Project(string name, int value)
  : SmartEnum<Project>(name, value)
{
    public static readonly Project Core = new(nameof(Core), 1);
    public static readonly Project Infrastructure = new(nameof(Infrastructure), 2);
    public static readonly Project UseCases = new(nameof(UseCases), 3);
    public static readonly Project Web = new(nameof(Web), 4);
}

