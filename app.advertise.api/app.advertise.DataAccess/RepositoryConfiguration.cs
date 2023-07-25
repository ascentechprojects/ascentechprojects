using app.advertise.DataAccess.ConnectionStrings;
using app.advertise.DataAccess.Repositories.Admin;
using app.advertise.DataAccess.Repositories.Vendor;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddTransient<IAdminUserRepository, AdminUserRepository>();
            services.AddTransient<IHoardingMasterRepository,HoardingMasterRepository>();
            services.AddTransient<IUpdateStatusRespository, UpdateStatusRespository>();
            services.AddScoped(typeof(IListItemRepository<>), typeof(ListItemRepository<>));
            services.AddTransient<IApplicationMasterRespository, ApplicationMasterRespository>();
            services.AddTransient<IAdminDashboardRepository, AdminDashboardRepository>();

            services.AddTransient<IApplicationRepository, ApplicationRepository>();
            return services;

        }
    }
}
