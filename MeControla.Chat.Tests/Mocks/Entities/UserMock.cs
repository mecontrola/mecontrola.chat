using MeControla.Chat.Data.Entities;
using System.Collections.Generic;
using data = MeControla.Chat.Tests.Mocks.Datas.UserData;

namespace MeControla.Chat.Tests.Mocks.Entities
{
    public static class UserMock
    {
        public static IList<User> CreateList()
            => new List<User>
            {
                Create()
            };

        public static User Create()
        {
            var room = RoomMock.Create();
            return new User
            {
                Id = data.Id,
                Uuid = data.Uuid,
                Name = data.Name,
                RoomId = room.Id,
                Room = room
            };
        }
    }
}