using System;

namespace MeControla.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name)
            : base($"The {name} not found.")
        { }
    }
}