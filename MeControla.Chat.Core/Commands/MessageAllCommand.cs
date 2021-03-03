namespace MeControla.Chat.Core.Commands
{
    public class MessageAllCommand : ICommand
    {
        public string Message { get; }

        public MessageAllCommand(string identifier, string value)
        {
            Message = $"{identifier} {value}";
        }
    }
}