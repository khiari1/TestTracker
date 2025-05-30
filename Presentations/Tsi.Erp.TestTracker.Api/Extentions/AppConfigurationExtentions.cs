namespace Tsi.Erp.TestTracker.Api.Extentions
{
    public static class AppConfigurationExtentions
    {
        public static T GetSection<T>(this IConfiguration configuration, string? name)
        {
            return configuration.GetSection(name).Get<T>();
        }

        public static T GetSection<T>(this IConfiguration configuration)
        {
            return configuration.GetSection(typeof(T).Name).Get<T>();
        }
    }
}
