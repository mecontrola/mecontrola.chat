namespace MeControla.Chat.Core.Commands
{
    public class ConnectCommand : ICommand
    {
        public string Username { get; }
        public string Room { get; }

        public ConnectCommand(string identifier, string value)
        {
            Username = identifier;
            Room = value;
        }
    }
}