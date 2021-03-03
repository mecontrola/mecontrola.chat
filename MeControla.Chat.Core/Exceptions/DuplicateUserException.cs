using System;

namespace MeControla.Chat.Core.Exceptions
{
    public class DuplicateUserException : Exception
    {
        public DuplicateUserException(string name)
            : base($"Exists a username {name} used. Choose a other username to join.")
        { }
    }
}