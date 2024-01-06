using Application.Booking.Dtos;
using Application.Payment.Responses;
using MediatR;

namespace Application.Booking.Commands
{
    public class PayForABookingCommand : IRequest<PaymentResponse>
    {
        public PaymentRequestDto PaymentRequestDto { get; set; }
    }
}
