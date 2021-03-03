using MeControla.Chat.Core.Builders;
using MeControla.Chat.Core.Exceptions;
using MeControla.Chat.Core.Mappers.EntityToDto;
using MeControla.Chat.Data.Dtos;
using MeControla.Chat.Data.Entities;
using MeControla.Chat.DataStorage.Repositories;
using MeControla.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeControla.Chat.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRoomRepository roomRepository;
        private readonly IUserRepository userRepository;
        private readonly IRoomUsersEntityToDtoMapper roomUsersEntityToDtoMapper;
        private readonly IUserEntityToDtoMapper userEntityToDtoMapper;

        public UserService(IRoomRepository roomRepository,
                           IUserRepository userRepository,
                           IRoomUsersEntityToDtoMapper roomUsersEntityToDtoMapper,
                           IUserEntityToDtoMapper userEntityToDtoMapper)
        {
            this.roomRepository = roomRepository;
            this.userRepository = userRepository;
            this.roomUsersEntityToDtoMapper = roomUsersEntityToDtoMapper;
            this.userEntityToDtoMapper = userEntityToDtoMapper;
        }

        public async Task<UserDto> Join(string connectionId, string username, string roomName)
        {
            var room = await roomRepository.FindBydNameAsync(roomName);

            if (room == null)
                throw new NotFoundException(nameof(room));

            var user = await userRepository.FindByNameAsync(username);

            if (user != null)
                throw new DuplicateUserException(username);

            user = UserBuild(connectionId, username, room);

            await userRepository.CreateAsync(user);

            return userEntityToDtoMapper.ToMap(user);
        }

        public async Task<UserDto> Leave(string connectionId)
        {
            var user = await userRepository.FindByConnectionIdAsync(connectionId);

            if (user == null)
                throw new NotFoundException(nameof(user));

            try
            {
                await userRepository.RemoveAsync(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return userEntityToDtoMapper.ToMap(user);
        }

        public async Task<IList<RoomUsersDto>> GetAllUserFromRoom(string roomName)
        {
            var room = await roomRepository.FindBydNameAsync(roomName);

            if (room == null)
                throw new NotFoundException(nameof(room));

            var users = await userRepository.GetAllFromRoom(room.Id);

            return roomUsersEntityToDtoMapper.ToMapList(users);
        }

        public async Task<IList<RoomUsersDto>> GetUsersInRoom(string connectionId)
        {
            var user = await userRepository.FindByConnectionIdAsync(connectionId);

            if (user == null)
                throw new NotFoundException(nameof(user));

            var users = await userRepository.GetAllFromRoom(user.RoomId);

            return roomUsersEntityToDtoMapper.ToMapList(users);

        }

        public async Task<RoomChangeDto> ChangeRoom(string connectionId, string roomName)
        {
            var room = await roomRepository.FindBydNameAsync(roomName);

            if (room == null)
                throw new NotFoundException(nameof(room));

            var user = await userRepository.FindByConnectionIdAsync(connectionId);
            var roomOld = user.Room;
            user.RoomId = room.Id;
            user.Room = room;

            try
            {
                await userRepository.UpdateAsync(user);
            } catch (Exception e)
            { 
                Console.WriteLine(e.Message);
            }

            return new RoomChangeDto
            {
                User = user,
                RoomOld = roomOld
            };
        }

        private static User UserBuild(string connectionId, string username, Room room)
            => UserBuilder.GetInstance()
                          .SetConnectionId(connectionId)
                          .SetName(username)
                          .SetRoom(room)
                          .ToBuild();
    }
}