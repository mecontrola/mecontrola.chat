namespace MeControla.Core.Configurations
{
    internal class SwaggerConfiguration : ISwaggerConfiguration
    {
        public string JsonRoute { get; set; }
        public string Description { get; set; }
        public string UIEndpoint { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string RoutePrefix { get; set; }
        public bool Enabled { get; set; }
        public bool JWTAuthentication { get; set; }
    }
}