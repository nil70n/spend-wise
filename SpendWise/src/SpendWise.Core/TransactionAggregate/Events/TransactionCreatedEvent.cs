using Ardalis.SharedKernel;

namespace SpendWise.Core.TransactionAggregate.Events;

public sealed class TransactionCreatedEvent(Transaction transaction)
  : DomainEventBase
{
    public Transaction Transaction { get; init; } = transaction;
}

