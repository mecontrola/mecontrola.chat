using System;

namespace MeControla.Core.Configurations
{
    public interface IJWTConfiguration : IAppConfiguration
    {
        string Secret { get; }
        string Issuer { get; }
        string Audience { get; }
        TimeSpan TimeToExpire { get; }
    }
}