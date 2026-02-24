using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Entities;
using RevrenLove.Ledger.Persistence.SQLite;
using System.Collections;
using System.Linq.Expressions;

namespace RevrenLove.Ledger.Services;

public interface IDataAccessor<TEntityModel> : IQueryable<TEntityModel> where TEntityModel : class, IEntity
{
    Task<TEntityModel> CreateAsync(TEntityModel entity, bool saveChanges = true, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
    Task<TEntityModel> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<TEntityModel> GetAsync(Guid id, Func<IQueryable<TEntityModel>, IQueryable<TEntityModel>> configureQuery, CancellationToken cancellationToken);
    Task<ICollection<TEntityModel>> GetAsync(Func<IQueryable<TEntityModel>, IQueryable<TEntityModel>> configureQuery, CancellationToken cancellationToken = default);
    Task<ICollection<TEntityModel>> GetAsync(Guid? cursor, int? pageSize, Func<IQueryable<TEntityModel>, IQueryable<TEntityModel>> configureQuery, CancellationToken cancellationToken = default);
    Task<TEntityModel> UpdateAsync(TEntityModel entity, bool saveChanges = true, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

internal class DataAccessor<TEntityModel>(LedgerSQLiteDbContext dbContext) : IDataAccessor<TEntityModel> where TEntityModel : class, IEntity
{
    private IQueryable<TEntityModel> Query => DbContext.Set<TEntityModel>();

    private LedgerSQLiteDbContext DbContext => dbContext;

    public Type ElementType => Query.ElementType;

    public Expression Expression => Query.Expression;

    public IQueryProvider Provider => Query.Provider;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await DbContext.SaveChangesAsync(cancellationToken);

    public IEnumerator<TEntityModel> GetEnumerator() => Query.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public async Task<TEntityModel> GetAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = Query.IgnoreQueryFilters();

        try
        {
            return await query.SingleAsync(e => e.Id == id, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            var message = $"Entity of type {typeof(TEntityModel).Name} with Id '{id}' was not found.";

            throw new KeyNotFoundException(message, ex);
        }
    }

    public async Task<TEntityModel> GetAsync(
        Guid id,
        Func<IQueryable<TEntityModel>, IQueryable<TEntityModel>> configureQuery,
        CancellationToken cancellationToken)
    {
        var query = configureQuery(Query.IgnoreQueryFilters());

        try
        {
            return await query.SingleAsync(e => e.Id == id, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            var message = $"Entity of type {typeof(TEntityModel).Name} with Id '{id}' was not found.";

            throw new KeyNotFoundException(message, ex);
        }
    }

    public async Task<ICollection<TEntityModel>> GetAsync(
        Func<IQueryable<TEntityModel>, IQueryable<TEntityModel>> configureQuery,
        CancellationToken cancellationToken = default) =>
        await GetAsync(null, null, configureQuery, cancellationToken);

    public async Task<ICollection<TEntityModel>> GetAsync(
        Guid? cursor,
        int? pageSize,
        Func<IQueryable<TEntityModel>, IQueryable<TEntityModel>> configureQuery,
        CancellationToken cancellationToken = default)
    {
        var query = configureQuery(Query);

        if (cursor.HasValue)
        {
            var cursorEntity = await query.FirstOrDefaultAsync(e => e.Id == cursor.Value, cancellationToken);

            if (cursorEntity == null)
            {
                return [];
            }

            query = query.SkipWhile(e => e.Id != cursor.Value).Skip(1);
        }

        var entities = pageSize is not null
            ? await query.Take(pageSize.Value).ToListAsync(cancellationToken)
            : await query.ToListAsync(cancellationToken);

        return entities;
    }

    public async Task<TEntityModel> CreateAsync(TEntityModel entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntityModel>().Add(entity);

        if (saveChanges)
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }

        return entity;
    }

    public async Task DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var entity = await GetAsync(id, cancellationToken);

        DbContext.Set<TEntityModel>().Remove(entity);

        if (saveChanges)
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }

    // TODO: JE - See if this is the best way to handle updates
    public async Task<TEntityModel> UpdateAsync(TEntityModel entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var entry = DbContext.Entry(entity);

        if (entry.State == EntityState.Detached)
        {
            var existingEntity = await GetAsync(entity.Id, cancellationToken);

            DbContext.Entry(existingEntity).CurrentValues.SetValues(entity);

            entity = existingEntity;
        }

        if (saveChanges)
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }

        return entity;
    }
}
