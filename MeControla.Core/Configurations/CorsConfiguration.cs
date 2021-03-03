namespace MeControla.Core.Configurations
{
    public class CorsConfiguration : ICorsConfiguration
    {
        public string[] Origins { get; set; }
        public bool Enabled { get; set; }
    }
}