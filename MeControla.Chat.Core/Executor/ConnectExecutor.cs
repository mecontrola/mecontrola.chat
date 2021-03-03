using MeControla.Chat.Core.Commands;
using MeControla.Chat.Core.Services;
using MeControla.Chat.DataStorage.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeControla.Chat.Core.Executor
{
    public class ConnectExecutor : IConnectExecutor
    {
        private const string ROOM_DEFAULT = "global";

        private readonly IRoomService roomService;
        private readonly IUserService userService;

        public ConnectExecutor(IRoomService roomService, IUserService userService)
        {
            this.roomService = roomService;
            this.userService = userService;
        }

        public async Task<IResult> Execute(string connectionId, ICommand command)
        {
            if (command is ConnectCommand connectCommand)
                return await Execute(connectionId, connectCommand);
            else if (command is CreateRoomCommand createRoomCommand)
                return await Execute(connectionId, createRoomCommand);
            else if (command is ChangeRoomCommand changeRoomCommand)
                return await Execute(connectionId, changeRoomCommand);
            else if (command is MessageAllCommand messageAllCommand)
                return await Execute(connectionId, messageAllCommand);
            else if (command is MessagePrivateCommand messagePrivateCommand)
                return await Execute(connectionId, messagePrivateCommand);
            else if (command is MessagePublicCommand messagePublicCommand)
                return await Execute(connectionId, messagePublicCommand);
            else if (command is ListCommand listCommand)
                return await Execute(connectionId, listCommand);
            else if (command is ExitCommand exitCommand)
                return await Execute(connectionId, exitCommand);

            throw new NotImplementedException();
        }

        private async Task<ConnectResult> Execute(string connectionId, ConnectCommand command)
        {
            var room = GetRoomOrDefault(command);
            var user = await userService.Join(connectionId, command.Username, room);
            var userList = await userService.GetAllUserFromRoom(room);

            return new ConnectResult
            {
                Username = user.Name,
                Messages = userList.Select(itm => new MessageResult
                {
                    Message = $"O usuário {user.Name} entrou na sala.",
                    ConnectionId = itm.ConnectionId
                }).ToList()
            };
        }

        private async Task<MessageRoomResult> Execute(string connectionId, MessageAllCommand command)
        {
            var usersRoom = await userService.GetUsersInRoom(connectionId);
            var user = usersRoom.First(itm => itm.ConnectionId.Equals(connectionId));

            return new MessageRoomResult
            {
                UsernameFrom = user.Name,
                UsernameTo = "Todos",
                Messages = usersRoom.Select(itm => new MessageResult
                {
                    Message = command.Message,
                    ConnectionId = itm.ConnectionId
                }).ToList()
            };
        }

        private async Task<MessageRoomResult> Execute(string connectionId, MessagePublicCommand command)
        {
            var usersRoom = await userService.GetUsersInRoom(connectionId);
            var userFrom = usersRoom.First(itm => itm.ConnectionId.Equals(connectionId));
            var userTo = usersRoom.First(itm => itm.Name.Equals(command.Username));

            return new MessageRoomResult
            {
                UsernameFrom = userFrom.Name,
                UsernameTo = userTo.Name,
                Messages = usersRoom.Select(itm => new MessageResult
                {
                    Message = command.Message,
                    ConnectionId = itm.ConnectionId
                }).ToList()
            };
        }

        private async Task<MessageRoomResult> Execute(string connectionId, MessagePrivateCommand command)
        {
            var usersRoom = await userService.GetUsersInRoom(connectionId);
            var userFrom = usersRoom.First(itm => itm.ConnectionId.Equals(connectionId));
            var userTo = usersRoom.First(itm => itm.Name.Equals(command.Username));

            return new MessageRoomResult
            {
                UsernameFrom = userFrom.Name,
                UsernameTo = userTo.Name,
                Messages = new List<MessageResult>
                {
                    new MessageResult
                    {
                        Message = command.Message,
                        ConnectionId = userFrom.ConnectionId
                    },
                    new MessageResult
                    {
                        Message = command.Message,
                        ConnectionId = userTo.ConnectionId
                    }
                }
            };
        }

        private async Task<CreateRoomResult> Execute(string connectionId, CreateRoomCommand command)
        {
            await roomService.Create(command.Room);

            return new CreateRoomResult
            {
                ConnectionId = connectionId,
                Message = $"A sala {command.Room} foi criada."
            };
        }

        private async Task<ChangeRoomResult> Execute(string connectionId, ChangeRoomCommand command)
        {
            var change = await userService.ChangeRoom(connectionId, command.Room);
            var usersRoomOld = await userService.GetAllUserFromRoom(change.RoomOld.Name);
            var usersRoomnew = await userService.GetAllUserFromRoom(change.User.Room.Name);
            var msgRoomOld = usersRoomOld.Select(itm => new MessageResult
            {
                Message = $"O usuário {change.User.Name} saiu na sala.",
                ConnectionId = itm.ConnectionId
            });
            var msgRoomNew = usersRoomnew.Select(itm => new MessageResult
            {
                Message = $"O usuário {change.User.Name} entrou na sala.",
                ConnectionId = itm.ConnectionId
            });

            return new ChangeRoomResult
            {
                MessagesIn = msgRoomNew.ToList(),
                MessagesOut = msgRoomOld.ToList()
            };
        }

        private async Task<ListResult> Execute(string connectionId, ListCommand command)
        {
            var message = command.Type.Equals("rooms")
                        ? (await roomService.GetAll()).Select(itm => itm.Name)
                        : (await userService.GetUsersInRoom(connectionId))
                                            .Where(itm => !itm.ConnectionId.Equals(connectionId))
                                            .Select(itm => itm.Name);

            return new ListResult
            {
                ConnectionId = connectionId,
                Data = message.ToList()
            };
        }

        private async Task<ExitResult> Execute(string connectionId, ExitCommand _)
        {
            var user = await userService.Leave(connectionId);
            var userList = await userService.GetAllUserFromRoom(user.Room.Name);

            return new ExitResult
            {
                Messages = userList.Select(itm => new MessageResult
                {
                    Message = $"O usuário {user.Name} saiu na sala.",
                    ConnectionId = itm.ConnectionId
                }).ToList()
            };
        }

        public static string GetRoomOrDefault(ConnectCommand command)
            => string.IsNullOrWhiteSpace(command.Room)
             ? ROOM_DEFAULT
             : command.Room;
    }
}