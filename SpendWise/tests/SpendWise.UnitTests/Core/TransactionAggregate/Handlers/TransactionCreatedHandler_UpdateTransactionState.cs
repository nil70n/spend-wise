using System.Globalization;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SpendWise.Core.AccountAggregate;
using SpendWise.Core.Interfaces;
using SpendWise.Core.TransactionAggregate;
using SpendWise.Core.TransactionAggregate.Events;
using SpendWise.Core.TransactionAggregate.Handlers;
using Xunit;

namespace SpendWise.UnitTests.Core.TransactionAggregate.Handlers;

public class TransactionCreatedHandler_UpdateTransactionState
{
    private readonly IUpdateBalanceService _service = Substitute.For<IUpdateBalanceService>();
    private readonly IRepository<Transaction> _repository = Substitute.For<IRepository<Transaction>>();
    private readonly ILogger<TransactionCreatedHandler> _logger = Substitute.For<ILogger<TransactionCreatedHandler>>();

    private readonly TransactionCreatedHandler _handler;
    private readonly Account _account = CreateAccount();

    public TransactionCreatedHandler_UpdateTransactionState()
    {
        _service
          .UpdateBalance(Arg.Any<Transaction>(), Arg.Any<CancellationToken>())
          .Returns(s => AccountIdCheck(s.Arg<Transaction>()));

        _handler = new TransactionCreatedHandler(_service, _repository, _logger);
    }

    private Result AccountIdCheck(Transaction transaction)
    {
        return transaction.Account.Id == _account.Id
          ? Result.Success()
          : Result.NotFound();
    }

    private static Account CreateAccount()
    {
        return new Account("user", "account", new CultureInfo("en-US"), DateTime.UtcNow)
        {
            Id = Guid.NewGuid()
        };
    }

    private static Transaction CreateTransaction(Account account)
    {
        return new Transaction(account, 100, TransactionType.Debit, DateTime.UtcNow);
    }

    [Fact]
    public async Task DoesNotUpdateTransactionStateGivenUpdateBalanceFails()
    {
        var transaction = CreateTransaction(CreateAccount());

        await _handler.Handle(new TransactionCreatedEvent(transaction), CancellationToken.None);

        Assert.Equal(TransactionState.New, transaction.State);
    }

    [Fact]
    public async Task UpdatesTransactionStateGivenUpdateBalanceSucceeds()
    {
        var transaction = CreateTransaction(_account);

        await _handler.Handle(new TransactionCreatedEvent(transaction), CancellationToken.None);

        Assert.Equal(TransactionState.Calculated, transaction.State);
    }
}
