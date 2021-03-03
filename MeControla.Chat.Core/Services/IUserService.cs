using MeControla.Chat.Data.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeControla.Chat.Core.Services
{
    public interface IUserService
    {
        Task<UserDto> Join(string connectionId, string username, string roomName);
        Task<RoomChangeDto> ChangeRoom(string connectionId, string roomName);
        Task<IList<RoomUsersDto>> GetAllUserFromRoom(string roomName);
        Task<UserDto> Leave(string connectionId);
        Task<IList<RoomUsersDto>> GetUsersInRoom(string connectionId);
    }
}