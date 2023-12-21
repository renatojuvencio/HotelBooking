namespace Application
{
    public enum ErrorCode
    {
        //Codes related to Guests 1 - 99
        NOT_FOUND = 1,
        COULDNOT_STORE_DATA = 2,
        INVALID_ID_PERSON = 3,
        MISSING_REQUERED_INFORMATION = 4,
        INVALID_EMAIL = 5,
        GUES_NOT_FOUND = 6,

        //Codes related to Room 100 - 199
        ROOM_NOT_FOUND = 100,
        ROOM_COULDNOT_STORE_DATA = 101,
        ROOM_INVALID_ID_PERSON = 102,
        ROOM_MISSING_REQUERED_INFORMATION = 103,
        ROOM_INVALID_EMAIL = 104,
    }

    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorCode ErrorCode { get; set; }
    }
}
