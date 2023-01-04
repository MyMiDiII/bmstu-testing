using Microsoft.Extensions.DependencyInjection;
using ServerING.ModelConverters;

namespace ServerING.Utils {
    public static class ProvideExtension {
        public static IServiceCollection AddDtoConverters(this IServiceCollection services) {
            services.AddTransient<PlatformConverters>();
            services.AddTransient<ServerConverters>();
            services.AddTransient<CountryConverters>();
            services.AddTransient<HostingConverters>();
            services.AddTransient<PlayerConverters>();
            services.AddTransient<UserConverters>();

            return services;
        }
    }
}