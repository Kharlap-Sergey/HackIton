namespace Api.Domain;

public interface IReadOnlyRepo<TEntity, TEntityId> 
    where TEntity: Entity<TEntityId>
    where TEntityId: struct
{
    IAsyncEnumerable<TEntity> GetAllAsync();

    ValueTask<TEntity?> GetByIdAsync(TEntityId id);
}
