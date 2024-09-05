namespace Api.Domain;

public interface IRepo<TEntity, TKey> where TEntity : Entity
{
    ValueTask<TKey> AddAsync(TEntity entity);
}
 