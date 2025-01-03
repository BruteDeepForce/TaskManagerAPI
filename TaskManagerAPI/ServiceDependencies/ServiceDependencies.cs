using TaskManagerAPI.EntityRepositories;

namespace TaskManagerAPI.ServiceDependencies
{
    public static class ServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {

            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepositoryBase<>));   //EntityRepositoryBase'ı kullanabilmek için ekledim
            return services;
        }
    }
}
