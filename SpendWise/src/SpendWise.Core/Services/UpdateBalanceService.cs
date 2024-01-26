using Ardalis.Result;
using Ardalis.SharedKernel;
using Microsoft.Extensions.Logging;
using SpendWise.Core.AccountAggregate;
using SpendWise.Core.Interfaces;
using SpendWise.Core.TransactionAggregate;

namespace SpendWise.Core.Services;

public class UpdateBalanceService(
  IRepository<Account> repository,
  ILogger<UpdateBalanceService> logger) : IUpdateBalanceService
{
    public async Task<Result<int>> UpdateBalance(Transaction transaction, CancellationToken cancellationToken)
    {
        // TODO: Implement shared lock

        /*         var logScope = LoggerMessage.DefineScope("UpdateBalance"); */
        /*         var logStarted = LoggerMessage.Define<Guid, Guid>(LogLevel.Information, */
        /*                                                           new EventId(0), */
        /*                                                           "Balance Update started. Account: {transaction.Account.Id}, Transaction: {transaction.Id}"); */

        /*         logStarted(logger, transaction.Account.Id, transaction.Id, null); */


        logger.LogInformation("Balance Update started. Account: {transaction.Account.Id}, Transaction: {transaction.Id}",
                               transaction.Account.Id,
                               transaction.Id);


        var accountToUpdate = await repository.GetByIdAsync(transaction.Account.Id, cancellationToken);

        if (accountToUpdate is null)
        {
            logger.LogError(message: "Balance Update failed: Account {transaction.Account.Id} not found.",
                             args: transaction.Account.Id);

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
            logger.LogCritical(exception: ex,
                                message: "Failed to update Account Balance. {transaction}",
                                args: transaction);

            return Result.Error("Failed to update Account Balance.");
        }

        return Result.Success(newBalance, "Account Balance updated.");
    }
}

