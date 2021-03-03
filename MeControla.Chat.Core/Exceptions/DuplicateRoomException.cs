using System;

namespace MeControla.Chat.Core.Exceptions
{
    public class DuplicateRoomException : Exception
    {
        public DuplicateRoomException(string name)
            : base($"Exists a room {name} used. Choose a other room to name.")
        { }
    }
}