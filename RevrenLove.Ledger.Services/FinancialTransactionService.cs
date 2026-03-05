using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Services.Models;

namespace RevrenLove.Ledger.Services;

public interface IFinancialTransactionService
{
    Task<FinancialTransaction> GetAsync(Guid transactionId, CancellationToken cancellationToken = default);

    // TODO: JE - Make the `pageSize` not be magic
    // TODO: JE - Implement filters
    //Task<IEnumerable<FinancialTransaction>> GetAsync(FinancialTransactionStatus status, Guid? cursor = null, int pageSize = 25, CancellationToken cancellationToken = default);

    // TODO: JE - THIS IS TEMPORARY!!!
    [Obsolete("This method is temporary and will be removed in a future version.")]
    Task<IEnumerable<FinancialTransaction>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<IEnumerable<FinancialTransaction>> GetByFinancialAccountIdAsync(Guid financialAccountId, CancellationToken cancellationToken = default);

    // TODO: JE - Consider whether we want to have separate methods for creating transactions with vs. without associations, or if we want to have a single method that can handle both scenarios (e.g. by making the `associatedFinancialAccountId` parameter optional and handling the logic accordingly).
    Task<FinancialTransaction> CreateAsync(FinancialTransaction transaction, Guid? associatedFinancialAccountId, CancellationToken cancellationToken = default);

    Task<FinancialTransaction> UpdateAsync(FinancialTransaction transaction, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid transactionId, CancellationToken cancellationToken = default);

    // TODO: JE - Implement this method and the associated API endpoint. This will be used to associate transactions that were created separately (e.g. via a bank feed vs. manually by the user) or to change associations after creation.
    //Task AssociateFinancialTransactions(Guid financialTransactionId, Guid associatedFinancialTransactionId, CancellationToken cancellationToken = default);

    Task AssociateFinancialTransactionWithAccount(Guid financialTransactionId, Guid associatedFinancialAccountId, bool deleteExistingAssociatedTransaction = false, CancellationToken cancellationToken = default);
}

internal class FinancialTransactionService(
    IDataAccessor<Entities.FinancialTransaction> financialTransactions,
    Mapper mapper)
        : IFinancialTransactionService
{
    private readonly IDataAccessor<Entities.FinancialTransaction> _financialTransactions = financialTransactions;
    private readonly Mapper _mapper = mapper;

    //public Task AssociateFinancialTransactions(Guid financialTransactionId, Guid associatedFinancialTransactionId, CancellationToken cancellationToken = default)
    //{
    //    throw new NotImplementedException();
    //}

    public async Task AssociateFinancialTransactionWithAccount(
        Guid financialTransactionId,
        Guid associatedFinancialAccountId,
        bool deleteExistingAssociatedTransaction = false,
        CancellationToken cancellationToken = default)
    {
        var financialTransactionWithCorrelation = await GetFinancialTransactionWithCorrelationAsync(financialTransactionId, cancellationToken);

        if (financialTransactionWithCorrelation.CorrelatedTransaction != null)
        {
            if (deleteExistingAssociatedTransaction)
            {
                await _financialTransactions.DeleteAsync(financialTransactionWithCorrelation.CorrelatedTransaction.Id, saveChanges: false, cancellationToken);
            }
            else
            {
                financialTransactionWithCorrelation.CorrelatedTransaction.CorrelationId = null;
            }
        }

        var financialTransaction = _mapper.ToModel(financialTransactionWithCorrelation.Transaction);

        var associatedFinancialTransaction = await CreateAssociatedFinancialTransactionAsync(financialTransaction, associatedFinancialAccountId, saveChanges: false, cancellationToken);

        financialTransactionWithCorrelation.Transaction.CorrelationId = associatedFinancialTransaction.CorrelationId;

        await _financialTransactions.SaveChangesAsync(cancellationToken);
    }

    public async Task<FinancialTransaction> CreateAsync(FinancialTransaction transaction, Guid? associatedFinancialAccountId, CancellationToken cancellationToken = default)
    {
        var financialTransactionEntity = _mapper.ToEntity(transaction);

        Entities.FinancialTransaction? associatedFinancialTransactionEntity = null;

        if (associatedFinancialAccountId.HasValue)
        {
            associatedFinancialTransactionEntity =
                await
                    CreateAssociatedFinancialTransactionAsync(
                        transaction,
                        associatedFinancialAccountId.Value,
                        cancellationToken: cancellationToken);

            financialTransactionEntity.CorrelationId = associatedFinancialTransactionEntity.CorrelationId;
        }

        financialTransactionEntity = await _financialTransactions.CreateAsync(financialTransactionEntity, cancellationToken: cancellationToken);

        var financialTransactionWithCorrelation = new FinancialTransactionWithCorrelation(financialTransactionEntity, associatedFinancialTransactionEntity);

        transaction = _mapper.ToModel(financialTransactionWithCorrelation);

        return transaction;
    }

    public async Task<FinancialTransaction> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var financialTransactionWithCorrelation = await GetFinancialTransactionWithCorrelationAsync(id, cancellationToken);

        var financialTransaction = _mapper.ToModel(financialTransactionWithCorrelation);

        return financialTransaction;
    }

    public async Task<IEnumerable<FinancialTransaction>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var query =
            _financialTransactions
                .Include(t => t.FinancialAccount)
                .Where(t => t.FinancialAccount!.UserId == userId)
                .OrderByDescending(t => t.Date);

        var results = await GetQueryWithCorrelation(query).ToListAsync(cancellationToken);

        var financialTransactions = results.Select(_mapper.ToModel);

        return financialTransactions;
    }

    public async Task<IEnumerable<FinancialTransaction>> GetByFinancialAccountIdAsync(Guid financialAccountId, CancellationToken cancellationToken = default)
    {
        var query =
            _financialTransactions
                .Include(t => t.FinancialAccount)
                .Where(t => t.FinancialAccountId == financialAccountId)
                .OrderByDescending(t => t.Date);
        
        // TODO: JE - Look into projection in Mapperly
        var results = await GetQueryWithCorrelation(query).ToListAsync(cancellationToken);

        // BUG: JE - This will result in the `AssociatedTransaction` property being populated,
        // but the `FinancialAccount` property of the associated transaction will not be populated
        // since it's not included in the query. This is a limitation of how the correlation is
        // currently implemented and may need to be addressed in the future if we want to
        // support scenarios where the associated transaction's financial account information is needed.

        var financialTransactions = results.Select(_mapper.ToModel);
        
        return financialTransactions;
    }

    //public async Task<IEnumerable<FinancialTransaction>> GetAsync(
    //    FinancialTransactionStatus status,
    //    Guid? cursor = null,
    //    int pageSize = 25,
    //    CancellationToken cancellationToken = default)
    //{
    //    var query =
    //        _financialTransactions
    //            .Include(t => t.FinancialAccount)
    //            // TODO: JE - This will need to be modified to support filters instead of defaulting to date descending
    //            .OrderByDescending(t => t.Date)
    //            .AsQueryable();

    //    if (cursor.HasValue)
    //    {
    //        var cursorEntity = await _financialTransactions.FirstOrDefaultAsync(e => e.Id == cursor.Value, cancellationToken);

    //        if (cursorEntity == null)
    //        {
    //            return [];
    //        }

    //        query = query.SkipWhile(e => e.Id != cursor.Value).Skip(1);
    //    }

    //    query = query.Take(pageSize);

    //    var results = await GetQueryWithCorrelation(query).ToListAsync(cancellationToken);

    //    var financialTransactions = results.Select(_mapper.ToModel);

    //    return financialTransactions;
    //}

    public async Task<FinancialTransaction> UpdateAsync(FinancialTransaction transaction, CancellationToken cancellationToken = default)
    {
        var entity = await _financialTransactions.GetAsync(transaction.Id, cancellationToken);

        entity.Amount = transaction.Amount;
        entity.Description = transaction.Description;
        entity.Date = transaction.Date;

        await _financialTransactions.SaveChangesAsync(cancellationToken);

        return _mapper.ToModel(entity);
    }

    public async Task DeleteAsync(Guid transactionId, CancellationToken cancellationToken = default)
    {
        await _financialTransactions.DeleteAsync(transactionId, cancellationToken: cancellationToken);
    }

    private async Task<Entities.FinancialTransaction> CreateAssociatedFinancialTransactionAsync(
        FinancialTransaction transaction,
        Guid associatedFinancialAccountId,
        bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var correlationId = Guid.NewGuid();
        var associatedTransactionEntity = _mapper.ToEntity(transaction);

        associatedTransactionEntity.Id = Guid.Empty;
        associatedTransactionEntity.FinancialAccountId = associatedFinancialAccountId;
        associatedTransactionEntity.Amount = -transaction.Amount;
        associatedTransactionEntity.CorrelationId = correlationId;

        await _financialTransactions.CreateAsync(associatedTransactionEntity, saveChanges, cancellationToken);

        return associatedTransactionEntity;
    }

    private async Task<FinancialTransactionWithCorrelation> GetFinancialTransactionWithCorrelationAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = _financialTransactions.Include(t => t.FinancialAccount);

        try
        {
            var financialTransactionWithCorrelation =
                await
                    GetQueryWithCorrelation(query)
                        .SingleAsync(t => t.Transaction.Id == id, cancellationToken);

            return financialTransactionWithCorrelation;
        }
        catch (InvalidOperationException ex)
        {
            throw new KeyNotFoundException($"Financial transaction with Id '{id}' was not found.", ex);
        }
    }

    private IQueryable<FinancialTransactionWithCorrelation> GetQueryWithCorrelation(IQueryable<Entities.FinancialTransaction> query) =>
        query
            .GroupJoin(
                _financialTransactions.Include(t => t.FinancialAccount),
                t => t.CorrelationId,
                ct => ct.CorrelationId,
                (t, correlatedGroup) => new { Transaction = t, CorrelatedGroup = correlatedGroup })
            .SelectMany(
                x => x.CorrelatedGroup.Where(ct => ct.Id != x.Transaction.Id).DefaultIfEmpty(),
                (x, correlated) => new FinancialTransactionWithCorrelation(x.Transaction, correlated));
}
