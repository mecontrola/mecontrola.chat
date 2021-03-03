namespace MeControla.Chat.Core.Commands
{
    public class CreateRoomCommand : ICommand
    {
        public string Room { get; }

        public CreateRoomCommand(string identifier)
            => Room = identifier;
    }
}