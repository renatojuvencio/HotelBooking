namespace Application.Booking.Dtos
{
    public enum SupportedPaymentProveiders
    {
        Paypal = 1,
        Stripe = 2,
        PagSeguro = 3,
        MercadoPago = 4,
    }

    public enum SuportPaymentMethods
    {
        DebitCard = 1,
        CreditCard = 2,
        BankTransfer = 3,
    }
    public class PaymentRequestDto
    {
        public int BookingId { get; set; }
        public string PaymentIntention { get; set; }
        public SupportedPaymentProveiders SelectedPaymentProveiders { get; set; }
        public SuportPaymentMethods SuportPaymentMethods { get; set; }
    }
}
