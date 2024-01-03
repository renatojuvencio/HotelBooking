using Application.Booking.Ports;
using Application.Booking.Response;
using MediatR;

namespace Application.Booking.Commands
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, BookingResponse>
    {
        private IBookingManager _bookingManager;
        public CreateBookingHandler(IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;
        }
        public Task<BookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            return _bookingManager.CreateBookingAsync(request.BookingDto);
        }
    }
}
