using Ardalis.Result;
using Ardalis.SharedKernel;
using Microsoft.Extensions.Logging;
using SpendWise.Core.AccountAggregate;
using SpendWise.Core.Interfaces;
using SpendWise.Core.Services.LoggerDefinitions;
using SpendWise.Core.TransactionAggregate;

namespace SpendWise.Core.Services;

public class UpdateBalanceService(IRepository<Account> repository,
                                  ILogger<UpdateBalanceService> logger)
  : IUpdateBalanceService
{
    public async Task<Result<int>> UpdateBalance(Transaction transaction,
                                                 CancellationToken cancellationToken)
    {
        // TODO: Implement shared lock

        logger.UpdateBalanceStarted(transaction.Id, DateTime.UtcNow);

        var accountToUpdate = await repository.GetByIdAsync(transaction.Account.Id, cancellationToken);

        if (accountToUpdate is null)
        {
            logger.UpdateBalanceAccountNotFound(transaction.Account.Id, DateTime.UtcNow);
            return Result.NotFound("Account not found.");
        }

        var newBalance = transaction.Type == TransactionType.Credit
          ? accountToUpdate.Balance + transaction.Value
          : accountToUpdate.Balance - transaction.Value;

        accountToUpdate.UpdateBalance(newBalance);

        try
        {
            await repository.UpdateAsync(accountToUpdate, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.UpdateBalanceAcccountError(transaction.Id, DateTime.UtcNow, ex);
            return Result.CriticalError("Failed to update Account Balance.");
        }

        logger.UpdateBalanceAccountSuccess(transaction.Id, DateTime.UtcNow);
        return Result.Success(newBalance, "Account Balance updated.");
    }
}

