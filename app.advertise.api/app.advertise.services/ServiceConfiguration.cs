using app.advertise.services.Admin;
using app.advertise.services.Admin.Interfaces;
using app.advertise.services.Interfaces;
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
            return services;
        }
    }
}
