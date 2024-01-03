using Application.Booking.Dtos;
using Application.MercadoPago;
using Application.Payment.Ports;
using Payments.Application.Exceptions;


namespace Payments.Application
{
    public class PaymentProcessorFactory : IPaymentProcessorFactory
    {
        public IPaymentProcessor GetPaymentProcessor(SupportedPaymentProveiders supportedPaymentProveiders)
        {
            switch (supportedPaymentProveiders)
            {
                case SupportedPaymentProveiders.MercadoPago:
                    return new MercadoPagoAdapter();

                default: return new NotImplementedPaymentProvider();
            }
        }
    }
}
