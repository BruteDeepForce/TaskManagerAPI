using TaskManagerAPI.EntityRepositories;
using TaskManagerAPI.RedisIntegration;
using TaskManagerAPI.RedisModel;

namespace TaskManagerAPI.ServiceDependencies
{
    public static class ServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepositoryBase<>));   //EntityRepositoryBase'ı kullanabilmek için ekledim
            services.AddSingleton<IRedisCacheService, RedisCacheService>();   //redis servisini ekledim
            services.AddScoped<IRedisProcess, RedisProcess>();   //Redis içi işlemler servisi
            return services;
        }
    }
}
