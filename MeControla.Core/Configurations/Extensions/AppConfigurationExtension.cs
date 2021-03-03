namespace MeControla.Core.Configurations.Extensions
{
    public static class AppConfigurationExtension
    {
        public static bool IsEnabled(this IAppConfiguration appConfiguration)
            => appConfiguration != null && appConfiguration.Enabled;
    }
}