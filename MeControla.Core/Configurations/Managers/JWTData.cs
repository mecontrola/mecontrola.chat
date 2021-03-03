using System;

namespace MeControla.Core.Configurations.Managers
{
    internal class JWTData : IJWTData
    {
        public string Token { get; set; }
        public Guid Jti { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expired { get; set; }
    }
}