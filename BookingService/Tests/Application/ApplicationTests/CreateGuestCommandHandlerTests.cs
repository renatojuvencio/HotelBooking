using Application;
using Application.Guest.Commands;
using Application.Guest.DTOs;
using Domain.Guest.Ports;
using Moq;

namespace ApplicationTests
{
    [TestFixture]
    public class CreateGuestCommandHandlerTests
    {
        private CreateGuestHandler GetCommand(Mock<IGuestRepository> guestRepository = null)
        {
            var _guestRepository = guestRepository ?? new Mock<IGuestRepository>();
            var command = new CreateGuestHandler(_guestRepository.Object);
            return command;
        }
        [Test]
        public async Task Should_Create_Guest()
        {
            var fakeGuest = new GuestDTO
            {
                Email = "teste@fake.com",
                IdNumber = "123456789",
                IdTypeCode = 1,
                Name = "Test",
                Surname = "Fake"
            };

            var command = new CreateGuestCommand
            {
                GuestDTO = fakeGuest,
            };

            var guestRepository = new Mock<IGuestRepository>();
            var handler = GetCommand(guestRepository);
            var res = await handler.Handle(command, CancellationToken.None);

            Assert.True(res.Success);
        }

        [Test]
        public async Task Should_Not_Create_MissingInformation()
        {
            var fakeGuest = new GuestDTO
            {
                Email = "teste@fake.com",
                IdNumber = "123456789",
                IdTypeCode = 1,
                Surname = "Fake"
            };

            var command = new CreateGuestCommand
            {
                GuestDTO = fakeGuest,
            };

            var guestRepository = new Mock<IGuestRepository>();
            var handler = GetCommand(guestRepository);
            var res = await handler.Handle(command, CancellationToken.None);

            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.GUEST_MISSING_REQUERED_INFORMATION);
        }
    }
}
