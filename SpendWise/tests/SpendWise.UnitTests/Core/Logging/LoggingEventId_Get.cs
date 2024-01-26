using SpendWise.Core.Logging;
using SpendWise.Core.Logging.Scopes;
using Xunit;

namespace SpendWise.UnitTests.Core.Logging;

public class LoggingEventId_Get
{
    [Theory]
    [InlineData("CoreServices", 1, "NewLog")]
    [InlineData("CoreServices", 1, "Error")]
    [InlineData("CoreServices", 0, "Process Started")]
    [InlineData("CoreTransaction", 1, "NewLog")]
    [InlineData("CoreTransaction", 1, "Error")]
    [InlineData("CoreTransaction", 0, "Process Started")]
    [InlineData("UseCasesAccount", 1, "NewLog")]
    [InlineData("UseCasesAccount", 1, "Error")]
    [InlineData("UseCasesAccount", 0, "Process Started")]
    [InlineData("WebTransaction", 1, "NewLog")]
    [InlineData("WebTransaction", 1, "Error")]
    [InlineData("WebTransaction", 0, "Process Started")]
    public void ReturnsEventIdGivenScope(string scopeName, int id, string name)
    {
        var applicationScope = ApplicationScope.FromName(scopeName);
        var eventId = LoggingEventId.Get(applicationScope, id, name);

        Assert.Equal(name, eventId.Name);
        Assert.Equal((applicationScope.Value * 100) + id, eventId.Id);
    }
}
