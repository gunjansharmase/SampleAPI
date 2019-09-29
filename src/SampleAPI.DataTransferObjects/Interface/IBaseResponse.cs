using System;
using System.Collections.Generic;
using System.Text;

namespace SampleAPI.DataTransferObjects.Interface
{
    public interface IBaseResponse<T>
    {
        bool Success { get; }
        string Message { get; }
        IEnumerable<T> Results { get; }
        void SetFailure(string message);
        void SetMessage(string message);
        void SetSuccess();
        void SetSuccess(IEnumerable<T> results);
        void SetFailure(IEnumerable<T> results);
    }
}
