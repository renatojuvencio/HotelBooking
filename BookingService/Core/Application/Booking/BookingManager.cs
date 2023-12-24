using Application.Booking.Dtos;
using Application.Booking.Ports;
using Application.Booking.Requests;
using Application.Booking.Response;
using Domain.Booking.Ports;
using Domain.Guest.Ports;
using Domain.Room.Ports;

namespace Application.Booking
{
    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IGuestRepository _guestRepository;
        public BookingManager(IBookingRepository bookingRepository, IGuestRepository guestRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _guestRepository = guestRepository;
        }

        public async Task<BookingResponse> CreateBookingAsync(CreateBookingRequest request)
        {
            try
            {
                var booking = BookingDto.MapToEntity(request.Data);
                booking.Guest = await _guestRepository.Get(booking.Guest.Id);
                booking.Room = await _roomRepository.Get(booking.Room.Id);
                await booking.SaveAsync(_bookingRepository);
                request.Data.Id = booking.Id;
                return new BookingResponse
                {
                    Data = request.Data,
                    Success = true
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<BookingResponse> GetBokingAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetAsync(bookingId);
            if (booking == null)
                throw new Exception();

            return new BookingResponse
            {
                Data = BookingDto.MapToDto(booking),
                Success = true
            };
        }
    }
}
