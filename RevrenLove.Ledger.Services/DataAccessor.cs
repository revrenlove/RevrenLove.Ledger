using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RevrenLove.Ledger.Abstractions;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Services;

// TODO: JE - Move this from the Service project... it doesn't rely on models or any other services...
internal class DataAccessor<T>(ILedgerDbContext dbContext) : IDataAccessor<T> where
    T : class, IEntity
{
    public IQueryable<T> Get(Expression<Func<T, bool>> predicate) =>
        dbContext.Set<T>().Where(predicate);

    public IQueryable<T> Get() => dbContext.Set<T>();

    public async Task<T?> GetAsync(Guid id) => await dbContext.FindAsync<T>(id);

    public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate) =>
        await Get(predicate).ToListAsync();

    public async Task<List<T>> GetAsync() => await Get().ToListAsync();

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
        T existingEntity = await GetAsync(entity.Id) ?? throw new KeyNotFoundException();

        var entry = dbContext.Entry(existingEntity);
        var updatedEntry = dbContext.Entry(entity);

        var properties = entry.Properties.Where(IsUpdatableProperty);

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
        T entity = await GetAsync(id) ?? throw new KeyNotFoundException();

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

    public async Task<int> SaveChangesAsync() => await dbContext.SaveChangesAsync();

    private async Task<int> SaveIfNeededAsync(bool isSave) => isSave ? await SaveChangesAsync() : 0;

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
