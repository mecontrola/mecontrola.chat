using FluentAssertions;
using MeControla.Chat.Core.Commands;
using MeControla.Chat.Core.Exceptions;
using System;
using Xunit;
using dataRoom = MeControla.Chat.Tests.Mocks.Datas.RoomData;
using dataUser = MeControla.Chat.Tests.Mocks.Datas.UserData;

namespace MeControla.Chat.Tests.Commands
{
    public class CommandFactoryTests
    {
        private const string MESSAGE = "todo mundo pode ver";
        private const string LIST_TYPE = "rooms";

        private static readonly string COMMAND_CONNECT = $"/connect {dataUser.Name} {dataRoom.Name}";
        private static readonly string COMMAND_MESSAGE_PUBLIC = $"/public {dataUser.Name} {MESSAGE}";
        private static readonly string COMMAND_MESSAGE_PRIVATE = $"/private {dataUser.Name} {MESSAGE}";
        private static readonly string COMMAND_CHANGE_ROOM = $"/changeroom {dataRoom.Name}";
        private static readonly string COMMAND_LIST = $"/list {LIST_TYPE}";
        private static readonly string COMMAND_EXIT = "/exit";
        private static readonly string COMMAND_ERROR = "teste";

        private readonly ICommandFactory commandFactory;

        public CommandFactoryTests()
        {
            commandFactory = new CommandFactory();
        }

        [Fact(DisplayName = "[CommandFactory.GetCommand] Deve retornar o comando do tipo connect quando informando.")]
        public void DeveRetornarCommandoConnect()
        {
            var command = commandFactory.GetCommand(COMMAND_CONNECT);
            command.Should().BeOfType<ConnectCommand>();

            var cmd = (ConnectCommand)command;
            cmd.Username.Should().Be(dataUser.Name);
            cmd.Room.Should().Be(dataRoom.Name);
        }

        [Fact(DisplayName = "[CommandFactory.GetCommand] Deve retornar o comando do tipo message public quando informando.")]
        public void DeveRetornarCommandoMensagemPublica()
        {
            var command = commandFactory.GetCommand(COMMAND_MESSAGE_PUBLIC);
            command.Should().BeOfType<MessagePublicCommand>();

            var cmd = (MessagePublicCommand)command;
            cmd.Username.Should().Be(dataUser.Name);
            cmd.Message.Should().Be(MESSAGE);
        }

        [Fact(DisplayName = "[CommandFactory.GetCommand] Deve retornar o comando do tipo message private quando informando.")]
        public void DeveRetornarCommandoMessagePrivate()
        {
            var command = commandFactory.GetCommand(COMMAND_MESSAGE_PRIVATE);
            command.Should().BeOfType<MessagePrivateCommand>();

            var cmd = (MessagePrivateCommand)command;
            cmd.Username.Should().Be(dataUser.Name);
            cmd.Message.Should().Be(MESSAGE);
        }

        [Fact(DisplayName = "[CommandFactory.GetCommand] Deve retornar o comando do tipo change room quando informando.")]
        public void DeveRetornarCommandoChangeRoom()
        {
            var command = commandFactory.GetCommand(COMMAND_CHANGE_ROOM);
            command.Should().BeOfType<ChangeRoomCommand>();

            var cmd = (ChangeRoomCommand)command;
            cmd.Room.Should().Be(dataRoom.Name);
        }

        [Fact(DisplayName = "[CommandFactory.GetCommand] Deve retornar o comando do tipo list quando informando.")]
        public void DeveRetornarCommandoList()
        {
            var command = commandFactory.GetCommand(COMMAND_LIST);
            command.Should().BeOfType<ListCommand>();

            var cmd = (ListCommand)command;
            cmd.Type.Should().Be(LIST_TYPE);
        }

        [Fact(DisplayName = "[CommandFactory.GetCommand] Deve retornar o comando do tipo exit quando informando.")]
        public void DeveRetornarCommandoExit()
        {
            var command = commandFactory.GetCommand(COMMAND_EXIT);
            command.Should().BeOfType<ExitCommand>();
        }

        [Fact(DisplayName = "[CommandFactory.GetCommand] Deve retornar exceção quando comando errado.")]
        public void DeveRetornarExcecaoQuandoComandoErrado()
        {
            Action command = () => commandFactory.GetCommand(COMMAND_ERROR);
            command.Should().Throw<CommandNotFoundException>();
        }

        [Fact(DisplayName = "[CommandFactory.GetCommand] Deve retornar exceção quando comando vazio.")]
        public void DeveRetornarExcecaoQuandoComandoVazio()
        {
            Action command = () => commandFactory.GetCommand(string.Empty);
            command.Should().Throw<CommandNotFoundException>();
        }
    }
}