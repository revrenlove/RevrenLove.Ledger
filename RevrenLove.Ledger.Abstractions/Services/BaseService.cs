// TODO: JE - This is the new file

using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Abstractions.Services;

public abstract class BaseService<TModel, TEntity>(IDataAccessor<TEntity> dataAccessor) where TEntity : class, IEntity
{
    public async Task<TModel> AddAsync(TModel model)
    {
        // TODO: JE - figure out a way to abstract the mapping
        TEntity entity = default!;

        await dataAccessor.AddAsync(entity);

        // TODO: JE - figure out a way to abstract the mapping
        // model = entity.ToModel();

        return model;
    }
}
