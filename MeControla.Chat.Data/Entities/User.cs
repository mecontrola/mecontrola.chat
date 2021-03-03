using MeControla.Core.Data.Entities;
using System;

namespace MeControla.Chat.Data.Entities
{
    public class User : IEntity
    {
        public long Id { get; set; }
        public Guid Uuid { get; set; }
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public long RoomId { get; set; }
        public Room Room { get; set; }
    }
}