using Application;
using Application.Payment.Ports;
using Application.Payment.Responses;

namespace Payments.Application.Exceptions
{
    public class NotImplementedPaymentProvider : IPaymentProcessor
    {
        public async Task<PaymentResponse> CapturePayment(string paymentIntation)
        {
            return new PaymentResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.PAYMENT_PROVIDER_NOT_IMPLEMENTED,
                Message = "The selected payment provider is not available at the moment"
            };
        }
    }
}
