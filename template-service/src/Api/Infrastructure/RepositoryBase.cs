using Api.Domain;
using Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure;

public class RepositoryBase<TEntity, TEntityId> : IReadOnlyRepo<TEntity, TEntityId>, IRepo<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : struct
{
    protected readonly DataDbContext _context;

    protected RepositoryBase(DataDbContext context)
    {
        _context = context;
    }

    public IAsyncEnumerable<TEntity> GetAllAsync()
    {
        return _context.Set<TEntity>()
            .AsAsyncEnumerable();
    }

    public async ValueTask<TEntity?> GetByIdAsync(TEntityId id)
    {
        return await _context.Set<TEntity>()
            .FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

    public async ValueTask<TEntityId> CreateAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await SaveChangesAsync();

        return entity.Id.Value;
    }

    public async ValueTask<bool> UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        return await SaveChangesAsync();
    }

    public async ValueTask<bool> DeleteAsync(TEntityId id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _context.Set<TEntity>().Remove(entity);
            return await SaveChangesAsync();
        }

        return false;
    }

    protected async ValueTask<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}
