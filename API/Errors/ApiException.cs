
namespace API.Errors
{
    public class ApiException
    {
        public ApiException(int statusCode, string message, string details)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
        // this class contain a response to a client what we have when our application have exception
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}