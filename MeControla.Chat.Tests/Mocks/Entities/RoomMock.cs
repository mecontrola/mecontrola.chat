using MeControla.Chat.Data.Entities;
using System.Collections.Generic;
using data = MeControla.Chat.Tests.Mocks.Datas.RoomData;

namespace MeControla.Chat.Tests.Mocks.Entities
{
    public static class RoomMock
    {
        public static IList<Room> CreateList()
            => new List<Room>
            {
                Create()
            };

        public static Room Create()
            => new Room
            {
                Id = data.Id,
                Uuid = data.Uuid,
                Name = data.Name
            };
    }
}