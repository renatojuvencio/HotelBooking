namespace Application
{
    public enum ErrorCode
    {
        NOT_FOUND = 1,
        COULDNOT_STORE_DATA = 2,
        INVALID_ID_PERSON = 3,
        MISSING_REQUERED_INFORMATION = 4,
        INVALID_EMAIL = 5,
        GUES_NOT_FOUND = 6
    }

    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorCode ErrorCode { get; set; }
    }
}
