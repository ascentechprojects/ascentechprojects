﻿using Microsoft.Extensions.DependencyInjection;

namespace app.advertise.DataAccess
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection Configure(IServiceCollection services)
        {
            services.AddSingleton<AdvertisementDbContext>();
            services.AddTransient<IHordingTypeConfigRepository,HordingTypeConfigRepository>();
            services.AddTransient<IHoardingtypeMasterRepository, HoardingtypeMasterRepository>();
            services.AddTransient<IDisplayTypeMasterRepository, DisplayTypeMasterRepository>();
            services.AddTransient<ILocationMasterRepository,LocationMasterRepository>();
            services.AddScoped<IAdminUserRepository, AdminUserRepository>();
            services.AddScoped<IHoardingMasterRepository,HoardingMasterRepository>();
            services.AddScoped<IUpdateStatusRespository, UpdateStatusRespository>();
            services.AddScoped(typeof(IListItemRepository<>), typeof(ListItemRepository<>));
            services.AddScoped<IApplicationMasterRespository, ApplicationMasterRespository>();
            return services;

        }
    }
}
