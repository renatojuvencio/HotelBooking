using Application.Booking;
using Application.Booking.Dtos;
using Application.Payment.Dtos;
using Application.Payment.Ports;
using Application.Payment.Responses;
using Domain.Booking.Ports;
using Domain.Guest.Ports;
using Domain.Room.Ports;
using Moq;

namespace ApplicationTests
{
    public class BookingManagerTest
    {
        [Test]
        public async Task Should_PayForBooking()
        {
            var dto = new PaymentRequestDto
            {
                SelectedPaymentProveiders = SupportedPaymentProveiders.MercadoPago,
                PaymentIntention = "https://www.mercadopago.comm.br/asdf",
                SuportPaymentMethods = SuportPaymentMethods.CreditCard
            };
            var bookingRepository = new Mock<IBookingRepository>();
            var roomsRepository = new Mock<IRoomRepository>();
            var guestRepository = new Mock<IGuestRepository>();
            var paymentProcessorFactory = new Mock<IPaymentProcessorFactory>();
            var paymentProcessor = new Mock<IPaymentProcessor>();

            var requestDto = new PaymentStateDto
            {
                CreatedDate = DateTime.Now,
                Message = $"Successfuly paid {dto.PaymentIntention}",
                PaymentId = "123",
                Status = PaymentStatus.Success
            };
            var response = new PaymentResponse
            {
                Data = requestDto,
                Success = true,
                Message = "Payment successfully processed"
            };
            paymentProcessor.Setup(x => x.CapturePayment(dto.PaymentIntention))
                .Returns(Task.FromResult(response));

            paymentProcessorFactory.Setup(x => x.GetPaymentProcessor(dto.SelectedPaymentProveiders))
                .Returns(paymentProcessor.Object);

            var bookingManager = new BookingManager(
                bookingRepository.Object,
                guestRepository.Object,
                roomsRepository.Object,
                paymentProcessorFactory.Object
                );
            var res = await bookingManager.PayForABooking(dto);
            Assert.NotNull(res);
            Assert.IsTrue(res.Success);
            Assert.AreEqual(res.Message, "Payment successfully processed");
            Console.WriteLine(res.Success + res.Message + res.Data);
        }
    }
}
