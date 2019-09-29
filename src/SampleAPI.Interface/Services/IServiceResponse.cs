using System.Collections.Generic;
using SampleAPI.Interface.DataAccess;

namespace SampleAPI.Interface.Services
{
    public interface IServiceResponse<T>
    {
        bool Success { get; }
        string Message { get; }
        T ResponseObject { get; }
        string Warning { get; }
        void SetFailure(string message);
        void SetMessage(string message);
        void SetSuccess();
        void SetSuccess(T responseObject);
        void SetFailure(T responseObject);

        IEnumerable<IDataValidationFailure> Errors { get; set; }
    }
}
