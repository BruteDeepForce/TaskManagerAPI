﻿using TaskManagerAPI.AdminNotify_Model;
using TaskManagerAPI.EntityRepositories;
using TaskManagerAPI.RedisIntegration;
using TaskManagerAPI.RedisModel;
using TaskManagerAPI.Register_Model;

namespace TaskManagerAPI.ServiceDependencies
{
    public static class ServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepositoryBase<>));   //EntityRepositoryBase'ı kullanabilmek için ekledim
            services.AddSingleton<IRedisCacheService, RedisCacheService>();   //redis servisini ekledim
            services.AddScoped<IRedisProcess, RedisProcess>();   //Redis içi işlemler servisi
            services.AddScoped<IRegisterService, Register>();   //Register servisi
            services.AddScoped<IAdminNotifyService, AdminNotify>();   //Admin notify servisi
            return services;
        }
    }
}
