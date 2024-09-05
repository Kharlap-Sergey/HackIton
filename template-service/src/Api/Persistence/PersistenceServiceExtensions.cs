using Microsoft.EntityFrameworkCore;

namespace Api.Persistence;

public static class PersistenceServiceExtensions
{
    public static IServiceCollection AddPgStorage(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DataDbContext>(options =>
            options.UseNpgsql(connectionString));
        return services;
    }
}
