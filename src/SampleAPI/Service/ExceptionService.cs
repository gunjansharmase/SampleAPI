using System;
using Microsoft.AspNetCore.Routing;
using SampleAPI.DataTransferObjects.DTO;
using SampleAPI.Interface.Services;

namespace SampleAPI.Service
{
    public class ExceptionService : IExceptionService
    {
        public UnhandledExceptionDTO GetUnhandledExceptionDTO(RouteData routeData, Exception ex)
        {
            string actionName = string.Empty;
            string controllerName = string.Empty;

            if (routeData.Values.ContainsKey("action"))
                actionName = routeData.Values["action"].ToString();

            if (routeData.Values.ContainsKey("controller"))
                controllerName = routeData.Values["controller"].ToString();

            return new UnhandledExceptionDTO
            {
                ActionName = actionName,
                ControllerName = controllerName,
                StackTrace = ex == null ? string.Empty : ex.ToString(),
                GUID = Guid.NewGuid().ToString()
            };
        }
    }
}
