using System;

namespace MeControla.Core.Configurations.Exceptions
{
    public class JWTTokenException : Exception
    {
        public JWTTokenException(string message)
            : base(message)
        { }
    }
}