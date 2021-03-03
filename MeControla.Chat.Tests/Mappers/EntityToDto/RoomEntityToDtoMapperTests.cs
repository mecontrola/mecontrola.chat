using FluentAssertions;
using MeControla.Chat.Core.Mappers.EntityToDto;
using MeControla.Chat.Data.Dtos;
using MeControla.Chat.Tests.Mocks.Dtos;
using MeControla.Chat.Tests.Mocks.Entities;
using System.Linq;
using Xunit;

namespace MeControla.Chat.Tests.Mappers.EntityToDto
{
    public class RoomEntityToDtoMapperTests
    {
        private IRoomEntityToDtoMapper mapper;

        public RoomEntityToDtoMapperTests()
        {
            mapper = new RoomEntityToDtoMapper();
        }

        [Fact(DisplayName = "[RoomEntityToDtoMapper.ToMap] Deve retornar null quando informado null.")]
        public void DeveRetornarNuloQuandoForNulo()
        {
            var actual = mapper.ToMap(null);

            Assert.Null(actual);
        }

        [Fact(DisplayName = "[RoomEntityToDtoMapper.ToMap] Deve retornar entidade quando informado dto preenchido.")]
        public void DeveRetornarPreenchidoQuandoPreenchido()
        {
            var entity = RoomMock.Create();
            var expected = RoomDtoMock.Create();
            var actual = mapper.ToMap(entity);

            AssertEqual(expected, actual);
        }

        [Fact(DisplayName = "[RoomEntityToDtoMapper.ToMap] Deve retornar lista de entidades quando informado lista de dtos preenchidas.")]
        public void DeveRetornarListaPreenchidaQuandoPreenchida()
        {
            var list = RoomMock.CreateList();
            var expected = RoomDtoMock.CreateList();
            var actual = mapper.ToMapList(list);

            AssertEqual(expected.FirstOrDefault(), actual.FirstOrDefault());
        }

        private static void AssertEqual(RoomDto expected, RoomDto actual)
        {
            actual.Id.Should().Be(expected.Id);
            actual.Name.Should().Be(expected.Name);
        }
    }
}