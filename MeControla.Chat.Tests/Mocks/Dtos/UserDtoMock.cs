using MeControla.Chat.Data.Dtos;
using System.Collections.Generic;
using data = MeControla.Chat.Tests.Mocks.Datas.UserData;

namespace MeControla.Chat.Tests.Mocks.Dtos
{
    public static class UserDtoMock
    {
        public static IList<UserDto> CreateList()
            => new List<UserDto>
            {
                Create()
            };

        public static UserDto Create()
            => new UserDto
            {
                Id = data.Uuid,
                Name = data.Name,
                Room = RoomDtoMock.Create()
            };
    }
}