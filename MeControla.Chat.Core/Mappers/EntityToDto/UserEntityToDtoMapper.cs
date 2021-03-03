using AutoMapper;
using MeControla.Chat.Data.Dtos;
using MeControla.Chat.Data.Entities;
using MeControla.Core.Mappers;

namespace MeControla.Chat.Core.Mappers.EntityToDto
{
    public class UserEntityToDtoMapper : BaseMapper<User, UserDto>, IUserEntityToDtoMapper
    {
        private readonly IRoomEntityToDtoMapper roomEntityToDtoMapper;

        public UserEntityToDtoMapper(IRoomEntityToDtoMapper roomEntityToDtoMapper)
        {
            this.roomEntityToDtoMapper = roomEntityToDtoMapper;
        }

        protected override IMappingExpression<User, UserDto> CreateMap(IMapperConfigurationExpression cfg)
        {
            return cfg.CreateMap<User, UserDto>()
                      .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Uuid))
                      .ForMember(dest => dest.ConnectionId, opt => opt.MapFrom(source => source.ConnectionId))
                      .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Name))
                      .ForMember(dest => dest.Room, opt => opt.MapFrom(source => roomEntityToDtoMapper.ToMap(source.Room)));
        }
    }
}