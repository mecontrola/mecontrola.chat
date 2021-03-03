using AutoMapper;
using MeControla.Chat.Data.Dtos;
using MeControla.Chat.Data.Entities;
using MeControla.Core.Mappers;

namespace MeControla.Chat.Core.Mappers.EntityToDto
{
    public class RoomEntityToDtoMapper : BaseMapper<Room, RoomDto>, IRoomEntityToDtoMapper
    {
        protected override IMappingExpression<Room, RoomDto> CreateMap(IMapperConfigurationExpression cfg)
        {
            return cfg.CreateMap<Room, RoomDto>()
                      .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Uuid))
                      .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Name));
        }
    }
}