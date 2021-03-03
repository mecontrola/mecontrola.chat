using MeControla.Chat.Data.Entities;

namespace MeControla.Chat.Core.Builders
{
    public class UserBuilder : BaseBuilder<UserBuilder, User>
    {
        public UserBuilder SetConnectionId(string connectionId)
        {
            obj.ConnectionId = connectionId;
            return this;
        }

        public UserBuilder SetName(string name)
        {
            obj.Name = name;
            return this;
        }

        public UserBuilder SetRoom(Room room)
        {
            obj.RoomId = room.Id;
            obj.Room = room;
            return this;
        }
    }
}