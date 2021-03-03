using MeControla.Chat.Data.Entities;

namespace MeControla.Chat.Data.Dtos
{
    public class RoomChangeDto
    {
        public User User { get; set; }
        public Room RoomOld { get; set; }
    }
}