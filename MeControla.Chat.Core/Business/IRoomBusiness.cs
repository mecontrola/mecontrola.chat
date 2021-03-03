using MeControla.Chat.Data.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeControla.Chat.Core.Business
{
    public interface IRoomBusiness
    {
        Task<IList<RoomDto>> GetAll();
    }
}