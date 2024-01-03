using Application.Booking.Dtos;
using Application.Booking.Ports;
using Application.Booking.Response;
using Application.Payment.Ports;
using Application.Payment.Responses;
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
        private readonly IPaymentProcessorFactory _paymentProcessorFactory;
        public BookingManager(IBookingRepository bookingRepository, IGuestRepository guestRepository, IRoomRepository roomRepository, IPaymentProcessorFactory paymentProcessorFactory)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _guestRepository = guestRepository;
            _paymentProcessorFactory = paymentProcessorFactory;
        }

        public async Task<BookingResponse> CreateBookingAsync(BookingDto request)
        {
            try
            {
                var booking = BookingDto.MapToEntity(request);
                booking.Guest = await _guestRepository.Get(booking.Guest.Id);
                booking.Room = await _roomRepository.Get(booking.Room.Id);
                await booking.SaveAsync(_bookingRepository);
                request.Id = booking.Id;
                return new BookingResponse
                {
                    Data = request,
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

        public async Task<PaymentResponse> PayForABooking(PaymentRequestDto paymentRequestDto)
        {
            var paymentProcessor = _paymentProcessorFactory.GetPaymentProcessor(paymentRequestDto.SelectedPaymentProveiders);
            var response = await paymentProcessor.CapturePayment(paymentRequestDto.PaymentIntention);

            if (response.Success)
            {
                return new PaymentResponse
                {
                    Data = response.Data,
                    Success = true,
                    Message = "Payment successfully processed"
                };
            }
            return response;
        }
    }
}
