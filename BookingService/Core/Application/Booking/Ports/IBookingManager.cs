using Application.Booking.Dtos;
using Application.Booking.Requests;
using Application.Booking.Response;
using Application.Payment.Responses;

namespace Application.Booking.Ports
{
    public interface IBookingManager
    {
        Task<BookingResponse> CreateBookingAsync(CreateBookingRequest request);
        Task<BookingResponse> GetBokingAsync(int bookingId);
        Task<PaymentResponse> PayForABooking(PaymentRequestDto paymentRequestDto);
    }
}
