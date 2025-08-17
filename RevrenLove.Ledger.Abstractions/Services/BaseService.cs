using RevrenLove.Ledger.Entities;

namespace RevrenLove.Ledger.Abstractions.Services;

public abstract class BaseService<TModel, TEntity>(IDataAccessor<TEntity> dataAccessor, IMapper mapper)
    : IBaseService<TModel, TEntity>
    where TEntity : class, IEntity
{
    protected IDataAccessor<TEntity> DataAccessor => dataAccessor;

    public async Task<TModel> AddAsync(TModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(model));

        var entity = mapper.Map<TEntity>(model);

        await dataAccessor.AddAsync(entity);

        model = mapper.Map<TModel>(entity);

        return model;
    }

    public async Task<TModel> GetAsync(Guid id)
    {
        var entity =
            await
                DataAccessor.GetAsync(id) ??
                    throw new KeyNotFoundException($"No {typeof(TEntity).FullName} found with Id of {id}");

        var model = mapper.Map<TModel>(entity);

        return model;
    }

    public async Task<TModel> UpdateAsync(TModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(model));

        var entity = mapper.Map<TEntity>(model);

        entity = await DataAccessor.UpdateAsync(entity);

        model = mapper.Map<TModel>(entity);

        return model;
    }
}
