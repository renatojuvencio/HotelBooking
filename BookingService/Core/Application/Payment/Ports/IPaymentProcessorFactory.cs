using Application.Booking.Dtos;

namespace Application.Payment.Ports
{
    public interface IPaymentProcessorFactory
    {
        IPaymentProcessor GetPaymentProcessor(SupportedPaymentProveiders SelectedPaymentProveiders);
    }
}
