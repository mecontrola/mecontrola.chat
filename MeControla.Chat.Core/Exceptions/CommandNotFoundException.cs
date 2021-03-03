using System;

namespace MeControla.Chat.Core.Exceptions
{
    public class CommandNotFoundException : Exception
    {
        private const string MESSAGE = "The informed command was not found.";

        public CommandNotFoundException()
            : base(MESSAGE)
        { }
    }
}