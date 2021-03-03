namespace MeControla.Core.Configurations
{
    public interface ISwaggerConfiguration : IAppConfiguration
    {
        string JsonRoute { get; }
        string Description { get; }
        string UIEndpoint { get; }
        string Title { get; }
        string Version { get; }
        string RoutePrefix { get; }
        bool JWTAuthentication { get; }
    }
}