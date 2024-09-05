using Api.Domain;

namespace Api.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddEntityRepo(this IServiceCollection services)
        {
            services.AddScoped<IRepo<Entity, int?>, EntityRepo>();
            services.AddScoped<IReadOnlyRepo<Entity, int>, EntityRepo>();
   
            return services;
        }
    }
}
