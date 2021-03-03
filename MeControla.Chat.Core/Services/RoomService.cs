using MeControla.Chat.Core.Builders;
using MeControla.Chat.Core.Exceptions;
using MeControla.Chat.Core.Mappers.EntityToDto;
using MeControla.Chat.Data.Dtos;
using MeControla.Chat.Data.Entities;
using MeControla.Chat.DataStorage.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeControla.Chat.Core.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository repository;
        private readonly IRoomEntityToDtoMapper mapper;

        public RoomService(IRoomRepository repository, IRoomEntityToDtoMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IList<RoomDto>> GetAll()
        {
            var list = await repository.FindAllAsync();
            return mapper.ToMapList(list);
        }

        public async Task Create(string roomName)
        {
            var room = await repository.FindBydNameAsync(roomName);

            if (room != null)
                throw new DuplicateRoomException(roomName);

            room = RoomBuild(roomName);

            await repository.CreateAsync(room);
        }

        private static Room RoomBuild(string roomName)
            => RoomBuilder.GetInstance()
                          .SetName(roomName)
                          .ToBuild();
    }
}