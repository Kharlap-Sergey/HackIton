using Api.Domain;
using Api.Persistence;

namespace Api.Infrastructure;

public class EntityRepo : IReadOnlyRepo<Entity, int>, IRepo<Entity, int?>
{
    private readonly DataDbContext _dataDbContext;

    public EntityRepo(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async ValueTask<int?> AddAsync(Entity entity)
    {
        _dataDbContext.MyEntities.Add(entity);
        await _dataDbContext.SaveChangesAsync();
        return entity.Id;
    }

    public IAsyncEnumerable<Entity> GetAsync()
    {
        return  _dataDbContext.MyEntities.AsAsyncEnumerable();
    }

    public ValueTask<Entity?> GetAsync(int id)
    {
        return _dataDbContext.MyEntities.FindAsync(id);
    }
}
