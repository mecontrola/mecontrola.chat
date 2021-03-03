namespace MeControla.Chat.Core.Commands
{
    public class MessagePrivateCommand : ICommand
    {
        public string Username { get; }
        public string Message { get; }

        public MessagePrivateCommand(string identifier, string value)
        {
            Username = identifier;
            Message = value;
        }
    }
}