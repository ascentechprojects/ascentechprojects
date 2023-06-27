using app.advertise.services.Admin;
using app.advertise.services.Admin.Interfaces;
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
            return services;
        }
    }
}
