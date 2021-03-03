using System;

namespace MeControla.Core.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
        { }

        public UnauthorizedException(Exception innerException)
            : base(null, innerException)
        { }
    }
}