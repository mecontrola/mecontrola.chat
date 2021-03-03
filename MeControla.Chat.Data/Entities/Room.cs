using MeControla.Core.Data.Entities;
using System;

namespace MeControla.Chat.Data.Entities
{
    public class Room : IEntity
    {
        public long Id { get; set; }
        public Guid Uuid { get; set; }
        public string Name { get; set; }
    }
}