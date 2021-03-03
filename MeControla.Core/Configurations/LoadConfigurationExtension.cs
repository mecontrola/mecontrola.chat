using MeControla.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace MeControla.Core.Configurations
{
    public static class LoadConfigurationExtension
    {
        public static ICorsConfiguration GetCorsConfiguration(this IConfiguration configuration)
            => configuration.GetConfigurationOrDefault<ICorsConfiguration, CorsConfiguration>();

        public static IJWTConfiguration GetJWTConfiguration(this IConfiguration configuration)
            => configuration.GetConfigurationOrDefault<IJWTConfiguration, JWTConfiguration>();

        public static ISwaggerConfiguration GetSwaggerConfiguration(this IConfiguration configuration)
            => configuration.GetConfigurationOrDefault<ISwaggerConfiguration, SwaggerConfiguration>();


        private static T GetConfigurationOrDefault<T, U>(this IConfiguration configuration)
            where U : T, new()
            => configuration.Load<U>() ?? new U();
    }
}