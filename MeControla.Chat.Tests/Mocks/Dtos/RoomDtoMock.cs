using MeControla.Chat.Data.Dtos;
using System.Collections.Generic;
using data = MeControla.Chat.Tests.Mocks.Datas.RoomData;

namespace MeControla.Chat.Tests.Mocks.Dtos
{
    public static class RoomDtoMock
    {
        public static IList<RoomDto> CreateList()
            => new List<RoomDto>
            {
                Create()
            };

        public static RoomDto Create()
            => new RoomDto
            {
                Id = data.Uuid,
                Name = data.Name
            };
    }
}