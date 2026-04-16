using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Services.Models;

namespace RevrenLove.Ledger.Services;

public interface IRecurringTransactionService
{
    Task<RecurringTransaction> GetAsync(Guid recurringTransactionId, CancellationToken cancellationToken = default);
    Task<IEnumerable<RecurringTransaction>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<RecurringTransaction> CreateAsync(RecurringTransaction recurringTransaction, CancellationToken cancellationToken = default);
    Task<RecurringTransaction> UpdateAsync(RecurringTransaction recurringTransaction, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

internal class RecurringTransactionService(
    IDataAccessor<Entities.RecurringTransaction> recurringTransactions,
    Mapper mapper)
        : IRecurringTransactionService
{
    private readonly IDataAccessor<Entities.RecurringTransaction> _recurringTransactions = recurringTransactions;
    private readonly Mapper _mapper = mapper;

    public async Task<RecurringTransaction> CreateAsync(RecurringTransaction recurringTransaction, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.ToEntity(recurringTransaction);

        entity = await _recurringTransactions.CreateAsync(entity, cancellationToken: cancellationToken);

        var model = _mapper.ToModel(entity);

        return model;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        await
            _recurringTransactions
                .DeleteAsync(id, cancellationToken: cancellationToken);

    public async Task<RecurringTransaction> GetAsync(Guid recurringTransactionId, CancellationToken cancellationToken = default)
    {
        var entity =
            await
                _recurringTransactions
                    .WithAccounts()
                    .SingleAsync(t => t.Id == recurringTransactionId, cancellationToken: cancellationToken);

        var model = _mapper.ToModel(entity);

        return model;
    }

    public async Task<IEnumerable<RecurringTransaction>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var entities =
            await
                _recurringTransactions
                    .WithAccounts()
                    .Where(t => t.FinancialAccount!.UserId == userId || t.DestinationFinancialAccount!.UserId == userId)
                    .ToListAsync(cancellationToken: cancellationToken);

        var models = entities.Select(_mapper.ToModel);

        return models;
    }

    public async Task<RecurringTransaction> UpdateAsync(RecurringTransaction recurringTransaction, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
