using Application.Payment.Ports;
using Application.Payment.Responses;
using MediatR;

namespace Application.Booking.Commands
{
    public class PayForABookingHandler : IRequestHandler<PayForABookingCommand, PaymentResponse>
    {
        private IPaymentProcessorFactory _paymentProcessorFactory;

        public PayForABookingHandler(IPaymentProcessorFactory paymentProcessorFactory)
        {
            _paymentProcessorFactory = paymentProcessorFactory;
        }

        public async Task<PaymentResponse> Handle(PayForABookingCommand request, CancellationToken cancellationToken)
        {
            var paymentProcessor = _paymentProcessorFactory.GetPaymentProcessor(request.PaymentRequestDto.SelectedPaymentProveiders);
            var response = await paymentProcessor.CapturePayment(request.PaymentRequestDto.PaymentIntention);

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
