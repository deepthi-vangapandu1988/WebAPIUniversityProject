using System.Net;

namespace WebApplication4.Models
{
    public class APIResponse
    {
        public APIResponse(HttpStatusCode statusCode, bool isSuccess, object data = null, List<string> errors = null)
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;
            Data = data;
            Errors = errors;
        }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;
        public object Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
