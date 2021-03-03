using AutoMapper;
using MeControla.Chat.Data.Dtos;
using MeControla.Chat.Data.Entities;
using MeControla.Core.Mappers;

namespace MeControla.Chat.Core.Mappers.EntityToDto
{
    public class RoomUsersEntityToDtoMapper : BaseMapper<User, RoomUsersDto>, IRoomUsersEntityToDtoMapper
    {
        protected override IMappingExpression<User, RoomUsersDto> CreateMap(IMapperConfigurationExpression cfg)
        {
            return cfg.CreateMap<User, RoomUsersDto>()
                      .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Uuid))
                      .ForMember(dest => dest.ConnectionId, opt => opt.MapFrom(source => source.ConnectionId))
                      .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Name));
        }
    }
}