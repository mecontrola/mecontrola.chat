using MeControla.Core.Data.Dtos;
using System;

namespace MeControla.Chat.Data.Dtos
{
    public class RoomUsersDto : IDto
    {
        public Guid Id { get; set; }
        public string ConnectionId { get; set; }
        public string Name { get; set; }
    }
}