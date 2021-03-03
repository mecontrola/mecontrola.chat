using MeControla.Chat.Data.Dtos;
using MeControla.Chat.Data.Entities;
using MeControla.Core.Mappers;

namespace MeControla.Chat.Core.Mappers.EntityToDto
{
    public interface IRoomUsersEntityToDtoMapper : IEntityToDtoMapper<User, RoomUsersDto>
    { }
}