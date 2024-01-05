using Application.Room.Commands;
using Application.Room.DTOs;
using Domain.Room.Ports;
using Moq;

namespace ApplicationTests
{
    [TestFixture]
    internal class CreateRoomCommandHandlerTests
    {
        private CreateRoomHandler GetCommandMock
            (
                Mock<IRoomRepository> roomRepository = null
            )
        {
            var _roomRepository = roomRepository ?? new Mock<IRoomRepository>();
            var command = new CreateRoomHandler
            (
                _roomRepository.Object
            );
            return command;
        }

        [Test]
        public async Task Should_Not_CreateRoom_If_MissingInformation()
        {
            var fakeRoom = new RoomDTO
            {
                InMaintenace = false
            };

            var command = new CreateRoomCommand
            {
                RoomDTO = fakeRoom
            };

            var roomRepository = new Mock<IRoomRepository>();
            var handler = GetCommandMock(roomRepository);
            var res = await handler.Handle(command, CancellationToken.None);

            Assert.False(res.Success);
        }

        [Test]
        public async Task Should_CreateRoom()
        {
            var fakeRoom = new RoomDTO
            {
                InMaintenace = false,
                HasGuest = false,
                IsAvailable = true,
                Level = 1,
                Name = "Test",
                Price = new Domain.Guest.ValueObjects.Price
                {
                    Currency = Domain.Guest.Enums.AcceptedCurrencies.Dollar,
                    Value = 25
                }
            };

            var command = new CreateRoomCommand
            {
                RoomDTO = fakeRoom
            };

            var roomRepository = new Mock<IRoomRepository>();
            var handler = GetCommandMock(roomRepository);
            var res = await handler.Handle(command, CancellationToken.None);

            Assert.True(res.Success);
        }
    }
}
