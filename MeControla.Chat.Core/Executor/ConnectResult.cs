using System.Collections.Generic;

namespace MeControla.Chat.Core.Executor
{
    public interface IResult { }

    public abstract class BaseResult
    {
        public string ConnectionId { get; set; }
    }

    public class ConnectResult : IResult
    {
        public string Username { get; set; }
        public IList<MessageResult> Messages { get; set; }
    }

    public class CreateRoomResult : MessageResult, IResult
    { }

    public class ChangeRoomResult : IResult
    {
        public string Username { get; set; }
        public IList<MessageResult> MessagesOut { get; set; }
        public IList<MessageResult> MessagesIn { get; set; }
    }

    public class MessageRoomResult : IResult
    {
        public string UsernameFrom { get; set; }
        public string UsernameTo { get; set; }
        public IList<MessageResult> Messages { get; set; }
    }

    public class ExitResult : IResult
    {
        public IList<MessageResult> Messages { get; set; }
    }

    public class ListResult : BaseResult, IResult
    {
        public IList<string> Data { get; set; }
    }

    public class MessageResult : BaseResult
    {
        public string Message { get; set; }
    }
}