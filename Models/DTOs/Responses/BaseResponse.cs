using System.Collections.Generic;
using System.Net;

namespace Models.DTOs.Responses
{
    public class BaseResponse
    {
        public HttpStatusCode Status { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }
        
        public static BaseResponse Ok() => 
            new()
            {
                Status = HttpStatusCode.OK
            };
        
    }
    
    public class BaseResponse<T> : BaseResponse
    {
        private BaseResponse() { }

        public BaseResponse(T data)
        {
            Data = data;
        }

        public T Data { get; set; }

        public static BaseResponse<T> Ok(T data)
        {
            return new BaseResponse<T>()
            {
                Data = data,
                Status = HttpStatusCode.OK
            };
        }
        
        public static BaseResponse Exception(HttpStatusCode statusCode, Dictionary<string, List<string>> errors)
        {
            return new BaseResponse()
            {
                Status = statusCode,
                Errors = errors
            };
        }
    }
}