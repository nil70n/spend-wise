using MediatR;
using Microsoft.Extensions.Logging;
using SpendWise.Core.Interfaces;
using SpendWise.Core.TransactionAggregate.Events;

namespace SpendWise.Core.TransactionAggregate.Handlers;

internal class TransactionCreatedHandler(IUpdateBalanceService _updateBalanceService,
                                         ILogger<TransactionCreatedHandler> _logger) : INotificationHandler<TransactionCreatedEvent>
{
    public async Task Handle(TransactionCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling Transaction Created event for {notification.Transaction.Id}", notification.Transaction.Id);

        await _updateBalanceService.UpdateBalance(notification.Transaction, cancellationToken);
    }
}

