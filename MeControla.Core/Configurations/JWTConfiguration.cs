using System;

namespace MeControla.Core.Configurations
{
    internal class JWTConfiguration : IJWTConfiguration
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan TimeToExpire { get; set; }
        public bool Enabled { get; set; }
    }
}