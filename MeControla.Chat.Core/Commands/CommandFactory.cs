using MeControla.Chat.Core.Exceptions;
using System.Text.RegularExpressions;

namespace MeControla.Chat.Core.Commands
{
    public class CommandFactory : ICommandFactory
    {
        private const string COMMAND_CONNECT = "connect";
        private const string COMMAND_MSGALL = "msgall";
        private const string COMMAND_PUBLIC = "public";
        private const string COMMAND_PRIVATE = "private";
        private const string COMMAND_CREATEROOM = "createroom";
        private const string COMMAND_CHANGEROOM = "changeroom";
        private const string COMMAND_LIST = "list";
        private const string COMMAND_EXIT = "exit";

        private const string GROUP_COMMAND = "command";
        private const string GROUP_IDENTIFIER = "identifier";
        private const string GROUP_VALUE = "value";

        private static readonly string COMMAND_PATTERN = $@"^\/(?<{GROUP_COMMAND}>[a-z]+)(\s(?<{GROUP_IDENTIFIER}>(\w|\d|\-)+)(\s(?<{GROUP_VALUE}>(.*)))?)?$";

        public ICommand GetCommand(string command)
        {
            var (strCommand, identifier, value) = GetCommandValues(command);

            if (strCommand.Equals(COMMAND_CONNECT))
                return new ConnectCommand(identifier, value);
            else if (strCommand.Equals(COMMAND_MSGALL))
                return new MessageAllCommand(identifier, value);
            else if (strCommand.Equals(COMMAND_PUBLIC))
                return new MessagePublicCommand(identifier, value);
            else if (strCommand.Equals(COMMAND_PRIVATE))
                return new MessagePrivateCommand(identifier, value);
            else if (strCommand.Equals(COMMAND_CREATEROOM))
                return new CreateRoomCommand(identifier);
            else if (strCommand.Equals(COMMAND_CHANGEROOM))
                return new ChangeRoomCommand(identifier);
            else if (strCommand.Equals(COMMAND_LIST))
                return new ListCommand(identifier);
            else if (strCommand.Equals(COMMAND_EXIT))
                return new ExitCommand();
            else
                throw new CommandNotFoundException();
        }

        private static (string, string, string) GetCommandValues(string command)
        {
            var matches = GetMatches(command);
            return matches.Count == 1
                 ? (GetValue(matches[0].Groups, GROUP_COMMAND), GetValue(matches[0].Groups, GROUP_IDENTIFIER), GetValue(matches[0].Groups, GROUP_VALUE))
                 : throw new CommandNotFoundException();
        }

        private static string GetValue(GroupCollection groups, string groupName)
            => groups.ContainsKey(groupName)
             ? groups[groupName].Value.Trim()
             : string.Empty;

        private static MatchCollection GetMatches(string command)
            => Regex.Matches(command, COMMAND_PATTERN);
    }
}