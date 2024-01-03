namespace Application
{
    public enum ErrorCodes
    {
        //Codes related to Guests 1 - 99
        GUEST_NOT_FOUND = 1,
        GUEST_COULDNOT_STORE_DATA = 2,
        GUEST_INVALID_ID_PERSON = 3,
        GUEST_MISSING_REQUERED_INFORMATION = 4,
        GUEST_INVALID_EMAIL = 5,

        //Codes related to Room 100 - 199
        ROOM_NOT_FOUND = 100,
        ROOM_COULDNOT_STORE_DATA = 101,
        ROOM_MISSING_REQUERED_INFORMATION = 102,

        //Codes reletead to Booking 200-499
        BOOKING_NOT_FOUND = 200,
        BOOKING_COULDNOT_STORE_DATA = 201,
        BOOKING_MISSING_REQUERED_INFORMATION = 202,

        //Payment related codes 500-150
        PAYMENT_INVALID_PAYMENT_INTENTION = 500,
        PAYMENT_PROVIDER_NOT_IMPLEMENTED = 501,
    }

    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }
}
