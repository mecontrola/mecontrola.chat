using System;

namespace MeControla.Core.Configurations.Managers
{
    public interface IJWTData
    {
        string Token { get; }
        Guid Jti { get; }
        DateTime Created { get; }
        DateTime Expired { get; }
    }
}