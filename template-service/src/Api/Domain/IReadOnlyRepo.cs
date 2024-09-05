namespace Api.Domain;

public interface IReadOnlyRepo<TEntity, TKey> where TEntity: Entity
{
    IAsyncEnumerable<TEntity> GetAsync();

    ValueTask<TEntity?> GetAsync(TKey id);
}
