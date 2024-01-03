using Application.Booking.Dtos;
using Application.Booking.Response;
using MediatR;

namespace Application.Booking.Commands
{
    public class CreateBookingCommand : IRequest<BookingResponse>
    {
        public BookingDto BookingDto { get; set; }
    }
}
