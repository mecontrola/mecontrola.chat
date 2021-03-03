using MeControla.Core.Data.Dtos;
using System;

namespace MeControla.Chat.Data.Dtos
{
    public class RoomDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}