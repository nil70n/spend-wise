using Ardalis.Result;
using Ardalis.SharedKernel;
using Microsoft.Extensions.Logging;
using SpendWise.Core.AccountAggregate;
using SpendWise.Core.Interfaces;
using SpendWise.Core.TransactionAggregate;

namespace SpendWise.Core.Services;

public class UpdateBalanceService(
  IRepository<Account> _repository,
  ILogger<UpdateBalanceService> _logger) : IUpdateBalanceService
{
    public async Task<Result<int>> UpdateBalance(Transaction transaction, CancellationToken cancellationToken)
    {
        // TODO: Implement shared lock

        _logger.LogInformation("Balance Update started. Account: {transaction.Account.Id}, Transaction: {transaction.Id}", transaction.Account.Id, transaction.Id);

        var accountToUpdate = await _repository.GetByIdAsync(transaction.Account.Id, cancellationToken);

        if (accountToUpdate is null)
        {
            _logger.LogInformation("Balance Update failed: Account {transaction.Account.Id} not found.", transaction.Account.Id);
            return Result.NotFound("Account not found.");
        }

        var newBalance = transaction.Type == TransactionType.Credit
          ? accountToUpdate.Balance + transaction.Value
          : accountToUpdate.Balance - transaction.Value;

        accountToUpdate.UpdateBalance(newBalance);

        await _repository.UpdateAsync(accountToUpdate, cancellationToken);

        return Result.Success(newBalance, "Account balance updated.");
    }
}

