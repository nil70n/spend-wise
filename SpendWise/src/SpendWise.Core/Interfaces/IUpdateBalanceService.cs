using Ardalis.Result;
using SpendWise.Core.TransactionAggregate;

namespace SpendWise.Core.Interfaces;

public interface IUpdateBalanceService
{
    Task<Result<int>> UpdateBalance(Transaction transaction,
                                    CancellationToken cancellationToken = default);
}
