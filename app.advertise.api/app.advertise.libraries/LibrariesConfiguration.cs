using app.advertise.libraries.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace app.advertise.libraries
{
    public static class LibrariesConfiguration
    {
        public static IServiceCollection Configure(IServiceCollection services)
        {
            services.AddSingleton<UserRequestHeaders>();
            services.AddSingleton<IInternalExceptionHandler, InternalExceptionHandler>();
            services.AddSingleton<DataProtectionPurpose>();
            services.AddSingleton<VendorRequestHeaders>();
            return services;
        }
    }
}
