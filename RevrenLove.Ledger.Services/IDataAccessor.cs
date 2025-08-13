using System.Linq.Expressions;
using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Services;

public interface IDataAccessor<T> where T : class, IEntity
{
    Task<T> AddAsync(T entity, bool isSave = true);
    Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities, bool isSave = true);
    IQueryable<T> Get(Expression<Func<T, bool>> predicate);
    IQueryable<T> Get();
    Task<T?> GetAsync(Guid id);
    Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> GetAsync();
    Task RemoveAsync(Guid id, bool isSave = true);
    Task<T> UpdateAsync(T entity, bool isSave = true);
    Task<int> SaveChangesAsync();
}
