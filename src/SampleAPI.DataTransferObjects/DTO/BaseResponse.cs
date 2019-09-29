using Newtonsoft.Json;
using System.Collections.Generic;
using SampleAPI.DataTransferObjects.Interface;


namespace SampleAPI.DataTransferObjects.DTO
{
    public class BaseResponse<T> : IBaseResponse<T>
    {   
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<T> Results { get; set; }

        [JsonConstructor]
        public BaseResponse(bool success, string message, IEnumerable<T> results)
        {
            Success = success;
            Message = message;
            Results = results;
        }

        public BaseResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public void SetSuccess()
        {
            Success = true;
        }

        public void SetFailure()
        {
            Success = false;
        }

        public void SetFailure(string message)
        {
            Message = message;
            Success = false;
        }

        public void SetMessage(string message)
        {
            Message = message;
        }

        public void SetResponseObject(IEnumerable<T> results)
        {
            Results = results;
        }

        public void SetSuccess(IEnumerable<T> results)
        {
            Success = true;
            Results = results;

        }

        public void SetFailure(IEnumerable<T> results)
        {
            Success = false;
            Results = results;
        }

    }
}
