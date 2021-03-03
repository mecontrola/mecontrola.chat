namespace MeControla.Chat.Core.Commands
{
    public class MessagePublicCommand : ICommand
    {
        public string Username { get; }
        public string Message { get; }

        public MessagePublicCommand(string identifier, string value)
        {
            Username = identifier;
            Message = value;
        }
    }
}