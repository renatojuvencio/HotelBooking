using NUnit.Framework;
using Payments.Application;
using Application.Booking.Dtos;
using Application.MercadoPago;
using Application;
namespace Payment.UnitTests
{
    public class MercadoPagoAdapterTests
    {
        [Test]
        public void ShouldReturn_MercadoPagoAdaper_Provider()
        {
            var factory = new PaymentProcessorFactory();
            var provider = factory.GetPaymentProcessor(SupportedPaymentProveiders.MercadoPago);
            Assert.AreEqual(provider.GetType(), typeof(MercadoPagoAdapter));
        }

        [Test]
        public async Task Should_Failed_WhenPaymentIntentionIsInvalid()
        {
            var factory = new PaymentProcessorFactory();
            var provider = factory.GetPaymentProcessor(SupportedPaymentProveiders.MercadoPago);
            var res = await provider.CapturePayment("");
            Assert.AreEqual(res.ErrorCode, ErrorCodes.PAYMENT_INVALID_PAYMENT_INTENTION);
        }

        [Test]
        public async Task Should_SuccessfullyProcessPayment()
        {
            var factory = new PaymentProcessorFactory();
            var provider = factory.GetPaymentProcessor(SupportedPaymentProveiders.MercadoPago);
            var res = await provider.CapturePayment("https://www.mercadopago.com.br/asdfasdfa");
            Assert.True(res.Success);
        }
    }
}
