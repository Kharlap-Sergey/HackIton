namespace Api.Domain;

public interface IRepo<TEntity, TEntityId> 
    where TEntity : Entity<TEntityId>
    where TEntityId : struct
{
    ValueTask<TEntityId> CreateAsync(TEntity entity);
    ValueTask<bool> UpdateAsync(TEntity entity);
}
 