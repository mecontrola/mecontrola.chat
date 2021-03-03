namespace MeControla.Chat.Core.Commands
{
    public class ListCommand : ICommand
    {
        public string Type { get; }

        public ListCommand(string identifier)
            => Type = identifier;
    }
}