using Application.Booking.Dtos;
using Application.Booking.Response;
using Domain.Booking.Exceptions;
using Domain.Booking.Ports;
using Domain.Guest.Ports;
using Domain.Room.Ports;
using MediatR;

namespace Application.Booking.Commands
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, BookingResponse>
    {
        private IBookingRepository _bookingRepository;
        private IRoomRepository _roomRepository;
        private IGuestRepository _guestRepository;
        public CreateBookingHandler(IGuestRepository guestRepository, IRoomRepository roomRepository, IBookingRepository bookingRepository)
        {
            _guestRepository = guestRepository;
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
        }
        public async Task<BookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var booking = BookingDto.MapToEntity(request.BookingDto);
                booking.Guest = await _guestRepository.Get(booking.Guest.Id);
                booking.Room = await _roomRepository.GetAggregate(booking.Room.Id);
                await booking.SaveAsync(_bookingRepository);
                request.BookingDto.Id = booking.Id;
                return new BookingResponse
                {
                    Data = request.BookingDto,
                    Success = true
                };
            }
            catch (PlacedAtIsARequiredInformationException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_MISSING_REQUERED_INFORMATION,
                    Message = "PlacedAt is a required information"
                };
            }
            catch (StartIsARequiredInformationException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_MISSING_REQUERED_INFORMATION,
                    Message = "start is a required information"
                };
            }
            catch (EndIsARequiredInformationException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_MISSING_REQUERED_INFORMATION,
                    Message = "end is a required information"
                };
            }
            catch (GuestIsARequiredInformationException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_MISSING_REQUERED_INFORMATION,
                    Message = "guest is a required information"
                };
            }
            catch (RoomIsARequiredInformationException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_MISSING_REQUERED_INFORMATION,
                    Message = "room is a required information"
                };
            }
            catch (Exception)
            {
                return new BookingResponse
                {
                    Success = false,
                    Message = "Could not create booking",
                    ErrorCode = ErrorCodes.BOOKING_COULDNOT_STORE_DATA
                };
            }
        }
    }
}
