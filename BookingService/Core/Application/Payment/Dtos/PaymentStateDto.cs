namespace Application.Payment.Dtos
{
    public enum PaymentStatus
    {
        Success = 1,
        Failed = 2,
        Error = 3,
        Undefined = 4
    }
    public class PaymentStateDto
    {
        public PaymentStatus Status { get; set; }
        public string PaymentId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Message { get; set; }
    }
}
