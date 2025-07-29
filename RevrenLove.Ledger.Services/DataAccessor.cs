using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RevrenLove.Ledger.Entities;
using RevrenLove.Ledger.Persistence;

namespace RevrenLove.Ledger.Services;

internal class DataAccessor<T>(ILedgerDbContext dbContext) : IDataAccessor<T> where
    T : class, IEntity, new()
{
    public async Task<T?> GetAsync(Guid id) => await dbContext.FindAsync<T>(id);

    public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate) =>
        await dbContext.Set<T>().Where(predicate).ToListAsync();

    public async Task<List<T>> GetAsync() => await dbContext.Set<T>().ToListAsync();

    public async Task<T> AddAsync(T entity, bool isSave = true)
    {
        await dbContext.AddAsync(entity);

        await SaveIfNeededAsync(isSave);

        return entity;
    }

    public async Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities, bool isSave = true)
    {
        await dbContext.AddRangeAsync(entities);

        await SaveIfNeededAsync(isSave);

        return entities;
    }

    public async Task<T> UpdateAsync(T entity, bool isSave = true)
    {
        T existingEntity =
            await GetAsync(entity.Id) ??
                throw new InvalidOperationException("Entity not found.");

        var entry = dbContext.Entry(existingEntity);
        var updatedEntry = dbContext.Entry(entity);

        var properties = entry.Properties.Where(p => IsUpdatableProperty(p));

        foreach (var property in properties)
        {
            var originalValue = property.CurrentValue;
            var updatedValue = updatedEntry.Property(property.Metadata.Name).CurrentValue;

            if (!Equals(originalValue, updatedValue))
            {
                property.CurrentValue = updatedValue;
                property.IsModified = true;
            }
        }

        await SaveIfNeededAsync(isSave);

        return existingEntity;
    }

    public async Task RemoveAsync(Guid id, bool isSave = true)
    {
        T entity = await GetAsync(id) ?? throw new InvalidOperationException("Entity not found.");

        if (entity is IActivable activable)
        {
            activable.IsActive = false;
        }
        else
        {
            dbContext.Remove(entity);
        }

        await SaveIfNeededAsync(isSave);
    }

    private async Task<int> SaveIfNeededAsync(bool isSave) =>
        isSave ? await dbContext.SaveChangesAsync() : 0;

    private static bool IsUpdatableProperty(PropertyEntry property)
    {
        var propInfo = property.Metadata.PropertyInfo;

        if (propInfo == null ||
            !propInfo.CanWrite ||
            (propInfo.PropertyType != typeof(string) && !propInfo.PropertyType.IsValueType))
        {
            return false;
        }

        return true;
    }
}
