using Application.MercadoPago.Exceptions;
using Application.Payment.Dtos;
using Application.Payment.Ports;
using Application.Payment.Responses;

namespace Application.MercadoPago
{
    public class MercadoPagoAdapter : IPaymentProcessor
    {
        public async Task<PaymentResponse> CapturePayment(string paymentIntention)
        {
            try
            {
                if (string.IsNullOrEmpty(paymentIntention) || string.IsNullOrWhiteSpace(paymentIntention))
                    throw new InvalidPaymentIntentionException();

                paymentIntention += "/success";
                return new PaymentResponse
                {
                    Data = new PaymentStateDto
                    {
                        CreatedDate = DateTime.Now,
                        PaymentId = "123",
                        Status = PaymentStatus.Success,
                        Message = $"Success paid {paymentIntention}"
                    },
                    Success = true
                };
            }
            catch (InvalidPaymentIntentionException)
            {
                return new PaymentResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PAYMENT_INVALID_PAYMENT_INTENTION
                };
            }
        }
    }
}
