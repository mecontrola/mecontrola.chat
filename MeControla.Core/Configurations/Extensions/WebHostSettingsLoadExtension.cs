using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace MeControla.Core.Configurations.Extensions
{
    public static class WebHostSettingsLoadExtension
    {
        public static IHostBuilder LoadApplicationSettings(this IHostBuilder builder)
        {
            return builder.ConfigureServices((context, services) =>
            {
                var corsConfiguration = context.Configuration.GetCorsConfiguration();
                services.TryAddSingleton(corsConfiguration);

                var jwtConfiguration = context.Configuration.GetJWTConfiguration();
                services.TryAddSingleton(jwtConfiguration);

                var swaggerConfiguration = context.Configuration.GetSwaggerConfiguration();
                services.TryAddSingleton(swaggerConfiguration);
            });
        }
    }
}