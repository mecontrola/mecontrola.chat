namespace MeControla.Chat.Core.Commands
{
    public class ChangeRoomCommand : CreateRoomCommand
    {
        public ChangeRoomCommand(string identifier)
            : base(identifier)
        { }
    }
}