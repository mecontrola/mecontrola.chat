namespace MeControla.Core.Configurations
{
    public interface ICorsConfiguration : IAppConfiguration
    {
        string[] Origins { get; }
    }
}