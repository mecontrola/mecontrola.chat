using FluentAssertions;
using MeControla.Chat.Core.Mappers.EntityToDto;
using MeControla.Chat.Data.Dtos;
using MeControla.Chat.Data.Entities;
using MeControla.Chat.Tests.Mocks.Dtos;
using MeControla.Chat.Tests.Mocks.Entities;
using NSubstitute;
using System.Linq;
using Xunit;

namespace MeControla.Chat.Tests.Mappers.EntityToDto
{
    public class UserEntityToDtoMapperTests
    {
        private readonly IUserEntityToDtoMapper mapper;

        public UserEntityToDtoMapperTests()
        {
            var mapperRoom = Substitute.For<IRoomEntityToDtoMapper>();
            mapperRoom.ToMap(Arg.Any<Room>()).Returns(RoomDtoMock.Create());

            mapper = new UserEntityToDtoMapper(mapperRoom);
        }

        [Fact(DisplayName = "[UserEntityToDtoMapper.ToMap] Deve retornar null quando informado null.")]
        public void DeveRetornarNuloQuandoForNulo()
        {
            var actual = mapper.ToMap(null);

            Assert.Null(actual);
        }

        [Fact(DisplayName = "[UserEntityToDtoMapper.ToMap] Deve retornar entidade quando informado dto preenchido.")]
        public void DeveRetornarPreenchidoQuandoPreenchido()
        {
            var entity = UserMock.Create();
            var expected = UserDtoMock.Create();
            var actual = mapper.ToMap(entity);

            AssertEqual(expected, actual);
        }

        [Fact(DisplayName = "[UserEntityToDtoMapper.ToMap] Deve retornar lista de entidades quando informado lista de dtos preenchidas.")]
        public void DeveRetornarListaPreenchidaQuandoPreenchida()
        {
            var list = UserMock.CreateList();
            var expected = UserDtoMock.CreateList();
            var actual = mapper.ToMapList(list);

            AssertEqual(expected.FirstOrDefault(), actual.FirstOrDefault());
        }

        private static void AssertEqual(UserDto expected, UserDto actual)
        {
            actual.Id.Should().Be(expected.Id);
            actual.Name.Should().Be(expected.Name);
            actual.Room.Id.Should().Be(expected.Room.Id);
            actual.Room.Name.Should().Be(expected.Room.Name);
        }
    }
}