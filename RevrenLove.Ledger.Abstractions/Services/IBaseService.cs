using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Abstractions.Services;

public interface IBaseService<TModel, TEntity> where TEntity : class, IEntity
{
    Task<TModel> AddAsync(TModel model);
    // TODO: JE - This needs a "where" or something.... probably
    Task<IEnumerable<TModel>> GetAsync();
    Task<TModel> GetAsync(Guid id);
    Task<TModel> UpdateAsync(TModel model);
}
