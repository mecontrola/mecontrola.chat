using MeControla.Chat.Core.Configurations;
using MeControla.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace MeControla.Chat.Core.Extensions
{
    public static class ConfigurationExtension
    {
        public static IAppConfiguration GetAppConfiguration(this IConfiguration configuration)
            => configuration.Load<AppConfiguration>();
    }
}