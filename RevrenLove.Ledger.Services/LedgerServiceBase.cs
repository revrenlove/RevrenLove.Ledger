using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Entities;
using RevrenLove.Ledger.Persistence.SQLite;

namespace RevrenLove.Ledger.Services;

public interface ILedgerServiceBase<TServiceModel, TEntityModel>
    where TServiceModel : class
    where TEntityModel : class, IEntity
{
    //Task<TServiceModel> CreateAsync(TServiceModel model, Action<TEntityModel>? configureEntity = null, CancellationToken cancellationToken = default);
    //Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    //Task<TServiceModel> GetAsync(Guid id, CancellationToken cancellationToken = default);
    //Task<ICollection<TServiceModel>> GetAsync(Guid? cursor, int pageSize, Func<IQueryable<TEntityModel>, IQueryable<TEntityModel>> configureQuery, CancellationToken cancellationToken = default);
    //Task<TServiceModel> UpdateAsync(TServiceModel model, Action<TEntityModel>? configureEntity = null, CancellationToken cancellationToken = default);
}

internal abstract class LedgerServiceBase<TServiceModel, TEntityModel>(LedgerSQLiteDbContext dbContext)
    where TServiceModel : class
    where TEntityModel : class, IEntity
{
    protected readonly LedgerSQLiteDbContext dbContext = dbContext;

    protected async Task<TServiceModel> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetEntityAsync(id, cancellationToken);
        var model = ToServiceModel(entity);

        return model;
    }

    protected async Task<ICollection<TServiceModel>> GetAsync(Func<IQueryable<TEntityModel>, IQueryable<TEntityModel>> configureQuery, CancellationToken cancellationToken = default) =>
        await GetAsync(null, null, configureQuery, cancellationToken);

    protected async Task<ICollection<TServiceModel>> GetAsync(Guid? cursor, int? pageSize, Func<IQueryable<TEntityModel>, IQueryable<TEntityModel>> configureQuery, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntityModel> query = dbContext.Set<TEntityModel>();

        query = configureQuery(query);

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

        var models = entities.Select(ToServiceModel).ToList();

        return models;
    }

    protected async Task<TServiceModel> CreateAsync(TServiceModel model, Action<TEntityModel>? configureEntity = null, CancellationToken cancellationToken = default)
    {
        var entity = ToEntity(model, configureEntity);

        dbContext.Set<TEntityModel>().Add(entity);

        await dbContext.SaveChangesAsync(cancellationToken);

        var createdModel = ToServiceModel(entity);

        return createdModel;
    }

    protected async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetEntityAsync(id, cancellationToken);

        dbContext.Set<TEntityModel>().Remove(entity);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    protected async Task<TServiceModel> UpdateAsync(TServiceModel model, Action<TEntityModel>? configureEntity = null, CancellationToken cancellationToken = default)
    {
        var entity = ToEntity(model, configureEntity);
        var entry = dbContext.Entry(entity);

        if (entry.State == EntityState.Detached)
        {
            var existingEntity = await GetEntityAsync(entity.Id, cancellationToken);
            entry = dbContext.Entry(existingEntity);

            foreach (var property in entry.Properties)
            {
                if (property.Metadata.IsPrimaryKey())
                {
                    continue;
                }

                var currentValue = property.CurrentValue;
                var newValue = entry.Property(property.Metadata.Name).Metadata.PropertyInfo?.GetValue(entity);

                if (!Equals(currentValue, newValue))
                {
                    property.CurrentValue = newValue;
                }
            }

            entity = existingEntity;
        }

        var modifiedProperties = entry.Properties
            .Where(p => p.IsModified)
            .ToList();

        if (modifiedProperties.Count > 0)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        var updatedModel = ToServiceModel(entity);

        return updatedModel;
    }

    protected abstract TServiceModel ToServiceModel(TEntityModel entity);

    protected abstract TEntityModel ToEntity(TServiceModel model, Action<TEntityModel>? configureEntity = null);

    protected async Task<TEntityModel> GetEntityAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = dbContext
            .Set<TEntityModel>()
            .IgnoreQueryFilters();

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
}
