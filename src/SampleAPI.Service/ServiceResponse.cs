using System.Collections.Generic;
using Newtonsoft.Json;
using SampleAPI.Interface.DataAccess;
using SampleAPI.Interface.Services;

namespace SampleAPI.Service
{
    public class ServiceResponse<T> : IServiceResponse<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T ResponseObject { get; set; }
        public string Warning { get; private set; }
        public IEnumerable<IDataValidationFailure> Errors { get; set; }

        public ServiceResponse(bool success, string message) : this(success, message, default(T))
        {
        }

        public ServiceResponse(bool success, string message, T responseObject) : this(success, message, responseObject, null)
        {
        }

        [JsonConstructor]
        public ServiceResponse(bool success, string message, T responseObject, IEnumerable<IDataValidationFailure> errors)
        {
            Success = success;
            Message = message;
            ResponseObject = responseObject;
            Errors = errors;
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

        public void SetResponseObject(T responseObject)
        {
            ResponseObject = responseObject;
        }

        public void SetSuccess(T responseObject)
        {
            Success = true;
            ResponseObject = responseObject;
        }

        public void SetFailure(T responseObject)
        {
            Success = false;
            ResponseObject = responseObject;
        }

        public void SetWarning(string warning)
        {
            Warning = warning;
        }
    }
}
