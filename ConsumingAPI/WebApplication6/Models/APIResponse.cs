using System.Net;

namespace WebApplication6.Models
{
    public class APIResponse
    {
        public APIResponse()
        {

        }
        public APIResponse(HttpStatusCode statusCode, bool isSuccess, List<StudentDTO> data = null, List<string> errors = null)
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;
            Data = data;
            Errors = errors;
        }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;
        public List<StudentDTO> Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
