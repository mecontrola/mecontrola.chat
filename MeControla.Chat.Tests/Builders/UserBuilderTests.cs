using FluentAssertions;
using MeControla.Chat.Core.Builders;
using MeControla.Chat.Tests.Mocks.Entities;
using Xunit;
using data = MeControla.Chat.Tests.Mocks.Datas.UserData;

namespace MeControla.Chat.Tests.Builders
{
    public class UserBuilderTests
    {
        [Fact(DisplayName = "[UserBuilder.ToBuild] Deve criar a entidade user preenchida com as informações necessárias.")]
        public void DeveGerarUserPreenchido()
        {
            var expected = UserMock.Create();
            var actual = UserBuilder.GetInstance()
                                    .SetName(data.Name)
                                    .SetRoom(RoomMock.Create())
                                    .ToBuild();

            actual.Name.Should().Be(expected.Name);
            actual.RoomId.Should().Be(expected.RoomId);
            actual.Room.Id.Should().Be(expected.Room.Id);
            actual.Room.Uuid.Should().Be(expected.Room.Uuid);
            actual.Room.Name.Should().Be(expected.Room.Name);
        }
    }
}