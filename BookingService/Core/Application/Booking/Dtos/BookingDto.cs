using Domain.Guest.Enums;
using Entities = Domain.Booking.Entities;

namespace Application.Booking.Dtos
{
    public class BookingDto
    {
        public BookingDto()
        {
            PlacedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        private Status Status { get; set; }
        public Status CurrentStatus { get; set; }

        public static Entities.Booking MapToEntity(BookingDto bookingDto)
        {
            return new Entities.Booking
            {
                Id = bookingDto.Id,
                Start = bookingDto.Start,
                End = bookingDto.End,
                Guest = new Domain.Guest.Entities.Guest { Id = bookingDto.GuestId },
                Room = new Domain.Room.Entities.Room { Id = bookingDto.RoomId },
                PlacedAt = bookingDto.PlacedAt
            };
        }

        public static BookingDto MapToDto(Entities.Booking booking)
        {
            return new BookingDto
            {
                Id = booking.Id,
                End = booking.End,
                GuestId = booking.Guest.Id,
                PlacedAt = booking.PlacedAt,
                RoomId = booking.Room.Id,
                Status = booking.Status,
                Start = booking.Start,
            };
        }
    }
}
