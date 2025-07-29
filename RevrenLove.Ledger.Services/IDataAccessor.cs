using System.Linq.Expressions;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Services;

public interface IDataAccessor<T> where T : class, IEntity, new()
{
    Task<T> AddAsync(T entity, bool isSave = true);
    Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities, bool isSave = true);
    Task<T?> GetAsync(Guid id);
    Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> GetAsync();
    Task RemoveAsync(Guid id, bool isSave = true);
    Task<T> UpdateAsync(T entity, bool isSave = true);
}
