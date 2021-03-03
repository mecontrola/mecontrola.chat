using MeControla.Chat.Data.Entities;

namespace MeControla.Chat.Core.Builders
{
    public class RoomBuilder : BaseBuilder<RoomBuilder, Room>
    {
        public RoomBuilder SetName(string name)
        {
            obj.Name = name;
            return this;
        }
    }
}