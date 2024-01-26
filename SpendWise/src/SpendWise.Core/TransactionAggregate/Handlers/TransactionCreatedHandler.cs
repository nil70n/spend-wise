using Ardalis.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;
using SpendWise.Core.Interfaces;
using SpendWise.Core.TransactionAggregate.Events;

namespace SpendWise.Core.TransactionAggregate.Handlers;

public class TransactionCreatedHandler(IUpdateBalanceService updateBalanceService,
                                         IRepository<Transaction> repository,
                                         ILogger<TransactionCreatedHandler> logger) : INotificationHandler<TransactionCreatedEvent>
{
    public async Task Handle(TransactionCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling Transaction Created event for {notification.Transaction.Id}",
                               notification.Transaction.Id);

        var result = await updateBalanceService.UpdateBalance(notification.Transaction, cancellationToken);

        if (result.Status != Ardalis.Result.ResultStatus.Ok)
        {
            return;
        }

        notification.Transaction.NextState();

        try
        {
            await repository.UpdateAsync(notification.Transaction, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(exception: ex,
                             message: "Failed to update Transaction State. {notification.Transaction}",
                             args: notification.Transaction);
        }
    }
}

