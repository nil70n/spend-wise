using Ardalis.Result;
using Ardalis.SharedKernel;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SpendWise.Core.AccountAggregate;
using SpendWise.Core.Services;
using SpendWise.Core.TransactionAggregate;
using Xunit;

namespace SpendWise.UnitTests.Core.Services;

public class UpdateBalanceService_UpdateBalance
{
    private readonly IRepository<Account> _repository = Substitute.For<IRepository<Account>>();
    private readonly ILogger<UpdateBalanceService> _logger = Substitute.For<ILogger<UpdateBalanceService>>();

    private readonly UpdateBalanceService _service;
    private readonly Guid _accountId = Guid.NewGuid();

    public UpdateBalanceService_UpdateBalance()
    {
        _repository
          .GetByIdAsync(_accountId, Arg.Any<CancellationToken>())
          .Returns(CreateAccount(_accountId));

        _service = new UpdateBalanceService(_repository, _logger);
    }

    private static Account CreateAccount(Guid accountId)
    {
        return new Account("user", "account", new System.Globalization.CultureInfo("en-US"), DateTime.UtcNow)
        {
            Id = accountId
        };
    }

    private static Transaction CreateTransaction(Account account, TransactionType type, int value)
    {
        return new Transaction(account, value, type, DateTime.UtcNow);
    }

    [Fact]
    public async Task ReturnsNotFoundGivenCantFindAccount()
    {
        var transaction = CreateTransaction(CreateAccount(Guid.NewGuid()), TransactionType.Credit, 1500);
        var result = await _service.UpdateBalance(transaction, CancellationToken.None);

        Assert.Equal(ResultStatus.NotFound, result.Status);
    }

    [Theory]
    [InlineData(true, 1500, 1500)]
    [InlineData(false, 1500, -1500)]
    public async Task UpdatesBalanceGivenTransaction(bool isCredit, int transactionValue, int expected)
    {
        var transactionType = isCredit ? TransactionType.Credit : TransactionType.Debit;
        var transaction = CreateTransaction(CreateAccount(_accountId), transactionType, transactionValue);
        var result = await _service.UpdateBalance(transaction, CancellationToken.None);

        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.Equal(expected, result.GetValue());
    }
}
