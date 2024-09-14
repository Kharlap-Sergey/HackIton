using Api.Domain;

namespace Api.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddEntityRepo(this IServiceCollection services)
        {
            services.AddScoped<ITelegramUserInfoRepository, TelegramUserInfoRepository>();
   
            return services;
        }
    }
}
