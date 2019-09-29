using System;
using Microsoft.AspNetCore.Routing;
using SampleAPI.DataTransferObjects.DTO;

namespace SampleAPI.Interface.Services
{
    public interface IExceptionService
    {
        UnhandledExceptionDTO GetUnhandledExceptionDTO(RouteData routeData, Exception ex);
    }
}
