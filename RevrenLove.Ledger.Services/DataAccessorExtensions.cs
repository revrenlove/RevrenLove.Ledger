using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Abstractions;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Services;

public static class DataAccessorExtensions
{
    public static async Task<T> ActivateAsync<T>(this IDataAccessor<T> dataAccessor, Guid id, bool isSave = true) where
        T : class, IActivable, IEntity
    {
        var entity = await dataAccessor.Get().FirstAsync(e => e.Id == id) ?? throw new KeyNotFoundException();

        entity.IsActive = true;

        if (isSave)
        {
            await dataAccessor.SaveChangesAsync();
        }

        return entity;
    }
}
