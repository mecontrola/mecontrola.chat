using MeControla.Chat.Core.Services;
using MeControla.Chat.Data.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeControla.Chat.Core.Business
{
    public class RoomBusiness : IRoomBusiness
    {
        private readonly IRoomService services;

        public RoomBusiness(IRoomService services)
        {
            this.services = services;
        }

        public async Task<IList<RoomDto>> GetAll()
            => await services.GetAll();
    }
}