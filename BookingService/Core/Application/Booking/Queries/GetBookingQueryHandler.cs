using Application.Booking.Dtos;
using Application.Booking.Response;
using Domain.Booking.Ports;
using MediatR;

namespace Application.Booking.Queries
{
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, BookingResponse>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingQueryHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task<BookingResponse> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetAsync(request.id);

            if(booking != null)
            {
                var bookingDto = BookingDto.MapToDto(booking);
                return new BookingResponse
                {
                    Data = bookingDto,
                    Success = true,
                };
            }

            return new BookingResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.BOOKING_NOT_FOUND,
                Message = $"Booking id {request.id} not found"
            };
        }
    }
}
