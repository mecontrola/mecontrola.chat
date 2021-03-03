namespace MeControla.Chat.Core.Commands
{
    public interface ICommandFactory
    {
        ICommand GetCommand(string command);
    }
}