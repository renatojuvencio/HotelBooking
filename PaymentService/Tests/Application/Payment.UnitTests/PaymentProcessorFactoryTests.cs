using Application;
using Application.Booking.Dtos;
using Application.MercadoPago;
using NUnit.Framework;
using Payments.Application;
using Payments.Application.Exceptions;

namespace Payment.UnitTests
{
    public class PaymentProcessorFactoryTests
    {
        [Test]
        public void ShouldReturn_NotImplementedPaymentProvider_WhenAskingForStripeProvider()
        {
            var factory = new PaymentProcessorFactory();
            var provider = factory.GetPaymentProcessor(SupportedPaymentProveiders.Stripe);
            Assert.AreEqual(provider.GetType(), typeof(NotImplementedPaymentProvider));
        }

        [Test]
        public void ShouldReturn_MercadoPagoAdapter_Provider()
        {
            var factory = new PaymentProcessorFactory();
            var provider = factory.GetPaymentProcessor(SupportedPaymentProveiders.MercadoPago);
            Assert.AreEqual(provider.GetType(), typeof(MercadoPagoAdapterTests));
        }

        [Test]
        public async Task ShouldReturnFalse_WhenCapturePaymentFor_NotImplementedProvider()
        {
            var factory = new PaymentProcessorFactory();
            var provider = factory.GetPaymentProcessor(SupportedPaymentProveiders.Stripe);
            var res = await provider.CapturePayment("https://www.mercadopago.com.br/asdfb");
            
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.PAYMENT_PROVIDER_NOT_IMPLEMENTED);
            Assert.AreEqual(res.Message, "The selected payment provider is not available at the moment");
        }
    }
}
