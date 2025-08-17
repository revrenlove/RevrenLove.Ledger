using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Abstractions.Services;

public interface IBaseService<TModel, TEntity> where TEntity : class, IEntity
{
    Task<TModel> AddAsync(TModel model);
    Task<TModel> GetAsync(Guid id);
    Task<TModel> UpdateAsync(TModel model);
}
