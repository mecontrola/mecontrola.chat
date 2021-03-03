using MeControla.Chat.Data.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeControla.Chat.Core.Services
{
    public interface IRoomService
    {
        Task Create(string roomName);
        Task<IList<RoomDto>> GetAll();
    }
}