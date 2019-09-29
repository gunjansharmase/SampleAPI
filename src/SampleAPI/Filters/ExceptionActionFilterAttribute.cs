using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using SampleAPI.DataTransferObjects.DTO;
using SampleAPI.Service;
using SampleAPI.Interface.Services;

namespace SampleAPI.Filters
{
    public class ExceptionActionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILog _logger;
        private readonly IExceptionService _exceptionService;
        public ExceptionActionFilterAttribute(ILog logger, IExceptionService exceptionService)
        {
            _logger = logger;
            _exceptionService = exceptionService;
        }

        public override void OnException(ExceptionContext context)
        {
            var unhandledExceptionDTO = _exceptionService.GetUnhandledExceptionDTO(context.RouteData, context.Exception);

            //Log undhandled exception DTO
            _logger.Error(unhandledExceptionDTO);

            //Construct the service reponse        
            var serviceResponse = new ServiceResponse<UnhandledExceptionDTO>(false, "Unhandled Exception details:", unhandledExceptionDTO);
            //What i noticed during my testing if i set ExceptionHandled to true it is setting 200 response code.
            //So making it false.<see>https://github.com/aspnet/Mvc/issues/5594</see> there is discussion on this.
            context.ExceptionHandled = false;
            context.HttpContext.Response.Clear();

            //Assuming unhandled once are InteralServerErrors
            var objectResult = new ObjectResult(serviceResponse);
            objectResult.ContentTypes.Add(new MediaTypeHeaderValue("application/json"));
            objectResult.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = objectResult;
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            await Task.Run(() =>
            {
                OnException(context);
            });
        }
    }
}
