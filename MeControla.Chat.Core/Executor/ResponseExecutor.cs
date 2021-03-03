using System;
using System.Collections.Generic;
using System.Linq;

namespace MeControla.Chat.Core.Executor
{
    public class ResponseExecutor : IResponseExecutor
    {
        private const string MESSAGE_LIST_EMPTY = "Lista vazia.";

        public IList<MessageResult> Generate(IResult result)
        {
            if (result is ConnectResult connectResult)
                return Generate(connectResult);
            else if (result is CreateRoomResult createRoomResult)
                return Generate(createRoomResult);
            else if (result is ChangeRoomResult changeRoomResult)
                return Generate(changeRoomResult);
            else if (result is MessageRoomResult messageRoomResult)
                return Generate(messageRoomResult);
            else if (result is ListResult listResult)
                return Generate(listResult);
            else if (result is ExitResult exitResult)
                return Generate(exitResult);

            throw new NotImplementedException();
        }

        private static IList<MessageResult> Generate(ConnectResult response)
        {
            return response.Messages.Select(itm => new MessageResult
            {
                ConnectionId = itm.ConnectionId,
                Message = CreateMessage(itm.Message)
            }).ToList();
        }

        private static IList<MessageResult> Generate(CreateRoomResult response)
            => CreateSingleItemList(new MessageResult
            {
                ConnectionId = response.ConnectionId,
                Message = response.Message
            });

        private static IList<MessageResult> Generate(ChangeRoomResult response)
            => new List<MessageResult>().Concat(response.MessagesOut)
                                        .Concat(response.MessagesIn)
                                        .Select(itm => new MessageResult
                                        {
                                            ConnectionId = itm.ConnectionId,
                                            Message = CreateMessage(itm.Message)
                                        })
                                        .ToList();

        private static IList<MessageResult> Generate(MessageRoomResult response)
            => response.Messages
                       .Select(itm => new MessageResult
                       {
                           ConnectionId = itm.ConnectionId,
                           Message = CreateMessage(response.UsernameFrom, response.UsernameTo, itm.Message)
                       })
                       .ToList();

        public static IList<MessageResult> Generate(ListResult response)
            => CreateSingleItemList(new MessageResult
            {
                ConnectionId = response.ConnectionId,
                Message = response.Data.Any()
                        ? string.Join(", ", response.Data)
                        : MESSAGE_LIST_EMPTY
            });

        public static IList<MessageResult> Generate(ExitResult response)
            => response.Messages;

        private static string CreateMessage(string usernameFrom, string usernameTo, string message)
            => $"[{DateTime.Now:HH:mm:ss}] {usernameFrom} say to {usernameTo}: {message}";

        private static string CreateMessage(string message)
            => $"[{DateTime.Now:HH:mm:ss}] {message}";

        private static IList<MessageResult> CreateSingleItemList(MessageResult response)
            => new List<MessageResult> { response };
    }
}