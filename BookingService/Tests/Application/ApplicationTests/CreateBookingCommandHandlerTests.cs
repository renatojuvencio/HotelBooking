using Application;
using Application.Booking.Commands;
using Domain.Booking.Entities;
using Domain.Booking.Ports;
using Domain.Guest.Entities;
using Domain.Guest.Ports;
using Domain.Room.Entities;
using Domain.Room.Ports;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTests
{
    [TestFixture]
    internal class CreateBookingCommandHandlerTests
    {
        private CreateBookingHandler GetCommandMock
            (
                Mock<IRoomRepository> roomRepository = null,
                Mock<IGuestRepository> guestRepository = null,
                Mock<IBookingRepository> bookingRepository = null
            )
        {
            var _roomRepository = roomRepository ?? new Mock<IRoomRepository>();
            var _guestRepository = guestRepository ?? new Mock<IGuestRepository>();
            var _bookingRepository = bookingRepository ?? new Mock<IBookingRepository>();

            var commandHandler = new CreateBookingHandler
            (
                _guestRepository.Object,
                _roomRepository.Object,
                _bookingRepository.Object
            );
            return commandHandler;
        }

        [Test]
        public async Task Should_Not_CreateBooking_If_RoomIsMissing()
        {
            var command = new CreateBookingCommand
            {
                BookingDto = new Application.Booking.Dtos.BookingDto
                {
                    GuestId = 1,
                    Start = DateTime.UtcNow,
                    End = DateTime.UtcNow.AddDays(1),
                }
            };

            var fakeGuest = new Guest
            {
                Id = command.BookingDto.GuestId,
                DocumentId = new Domain.Guest.ValueObjects.PersonId
                {
                    DocumentType = Domain.Guest.Enums.DocumentType.Passport,
                    IdNumber = "abc123"
                },
                Email = "fakeguest@teste.com",
                Name = "Fake",
                Surname = "Teste"
            };

            var guestRepository = new Mock<IGuestRepository>();
            guestRepository.Setup(x => x.Get(command.BookingDto.GuestId))
                .Returns(Task.FromResult(fakeGuest));

            var fakeBooking = new Booking
            {
                Id = 1,
            };

            var bookingRepoMock = new Mock<IBookingRepository>();
            bookingRepoMock.Setup(x => x.CreateAsync(It.IsAny<Booking>()))
                .Returns(Task.FromResult(fakeBooking.Id));

            var handler = GetCommandMock(null, guestRepository, bookingRepoMock);
            var res = await handler.Handle(command, CancellationToken.None);
            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.BOOKING_MISSING_REQUERED_INFORMATION);
        }

        [Test]
        public async Task Should_Not_CreateBooking_If_StartIsMissing()
        {
            var command = new CreateBookingCommand
            {
                BookingDto = new Application.Booking.Dtos.BookingDto
                {
                    GuestId = 1,
                    End = DateTime.UtcNow.AddDays(1),
                    RoomId = 1,
                }
            };

            var fakeGuest = new Guest
            {
                Id = command.BookingDto.GuestId,
                DocumentId = new Domain.Guest.ValueObjects.PersonId
                {
                    DocumentType = Domain.Guest.Enums.DocumentType.Passport,
                    IdNumber = "abc123"
                },
                Email = "fakeguest@teste.com",
                Name = "Fake",
                Surname = "Teste"
            };

            var guestRepository = new Mock<IGuestRepository>();
            guestRepository.Setup(x => x.Get(command.BookingDto.GuestId))
                .Returns(Task.FromResult(fakeGuest));

            var fakeRoom = new Room
            {
                Id = command.BookingDto.RoomId,
                InMaintenace = false,
                Price = new Domain.Guest.ValueObjects.Price
                {
                    Currency = Domain.Guest.Enums.AcceptedCurrencies.Dollar,
                    Value = 123
                },
                Name = "Fake room 101",
                Level = 1
            };

            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(x => x.Get(1))
                .Returns(Task.FromResult(fakeRoom));

            var fakeBooking = new Booking
            {
                Id = 1,
            };

            var bookingRepoMock = new Mock<IBookingRepository>();
            bookingRepoMock.Setup(x => x.CreateAsync(It.IsAny<Booking>()))
                .Returns(Task.FromResult(fakeBooking.Id));

            var handler = GetCommandMock(roomRepository, guestRepository, bookingRepoMock);
            var res = await handler.Handle(command, CancellationToken.None);
            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.BOOKING_MISSING_REQUERED_INFORMATION);
        }

        [Test]
        public async Task Should_CreateBooking()
        {
            var command = new CreateBookingCommand
            {
                BookingDto = new Application.Booking.Dtos.BookingDto
                {
                    GuestId = 1,
                    RoomId = 1,
                    End = DateTime.UtcNow.AddDays(1),
                    Start = DateTime.UtcNow,
                }
            };

            var fakeGuest = new Guest
            {
                Id = command.BookingDto.GuestId,
                DocumentId = new Domain.Guest.ValueObjects.PersonId
                {
                    DocumentType = Domain.Guest.Enums.DocumentType.Passport,
                    IdNumber = "abc123"
                },
                Email = "fakeguest@teste.com",
                Name = "Fake",
                Surname = "Teste"
            };

            var guestRepository = new Mock<IGuestRepository>();
            guestRepository.Setup(x => x.Get(command.BookingDto.GuestId))
                .Returns(Task.FromResult(fakeGuest));

            var fakeRoom = new Room
            {
                Id = command.BookingDto.RoomId,
                InMaintenace = false,
                Price = new Domain.Guest.ValueObjects.Price
                {
                    Currency = Domain.Guest.Enums.AcceptedCurrencies.Dollar,
                    Value = 123
                },
                Name = "Fake room 101",
                Level = 1,
            };

            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(x => x.Get(1))
                .Returns(Task.FromResult(fakeRoom));


            var fakeBooking = new Booking
            {
                Id = 1,
                Guest = fakeGuest,
                Room = fakeRoom
            };
            var bookingRepoMock = new Mock<IBookingRepository>();
            bookingRepoMock.Setup(x => x.CreateAsync(It.IsAny<Booking>()))
                .Returns(Task.FromResult(fakeBooking.Id));

            var handler = GetCommandMock(roomRepository, guestRepository, bookingRepoMock);
            var res = await handler.Handle(command, CancellationToken.None);
            Assert.NotNull(res);
            Assert.True(res.Success);
            Assert.Null(res.ErrorCode);
        }
    }
}
