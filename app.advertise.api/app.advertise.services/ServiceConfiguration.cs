using app.advertise.services.Admin;
using app.advertise.services.Admin.Interfaces;
using app.advertise.services.Interfaces;
using app.advertise.services.Vendor;
using app.advertise.services.Vendor.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace app.advertise.services
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection Configure(IServiceCollection services)
        {
            services.AddTransient<IHordingTypeConfigService, HordingTypeConfigService>();
            services.AddTransient<IHoardingtypeMasterService, HoardingtypeMasterService>();
            services.AddTransient<IDisplayTypeMasterService, DisplayTypeMasterService>();
            services.AddTransient<ILocationMasterService, LocationMasterService>();
            services.AddScoped<IOAuthService, OAuthService>();
            services.AddTransient<IHoardingMasterService, HoardingMasterService>();
            services.AddTransient<IListItemService, ListItemService>();
            services.AddTransient<IApplicationMasterService, ApplicationMasterService>();
            services.AddTransient<Admin.Interfaces.IDashboardService, Admin.DashboardService>();

            services.AddTransient<IApplicationService, ApplicationService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IoAuthService, oAuthService>();
            services.AddTransient<Vendor.Interfaces.IDashboardService, Vendor.DashboardService>();
            services.AddTransient<IAppliDocService, AppliDocService>();

            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
