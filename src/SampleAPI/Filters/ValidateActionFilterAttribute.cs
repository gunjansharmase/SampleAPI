using System;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SampleAPI.DataTransferObjects.DTO;
using SampleAPI.Service;
using SampleAPI.Interface.Services;

namespace SampleAPI.Filters
{
    public class ValidateActionFilterAttribute : ActionFilterAttribute
    {
        protected readonly ILog _logger;
        protected readonly IValidationService _validationService;

        public ValidateActionFilterAttribute(ILog loggerFactory, IValidationService validationService)
        {
            _logger = loggerFactory;
            _validationService = validationService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext actionContext,
            ActionExecutionDelegate next)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException(nameof(actionContext));
            }

            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            var isValid = await _validationService.ValidateAsync(actionContext.ActionArguments,
                actionContext.ActionDescriptor, actionContext.ModelState);

            if (!isValid)
            {
                actionContext.Result = new BadRequestObjectResult(new ServiceResponse<ValidationErrorDTO>(false,
                    "Validation Error", new ValidationErrorDTO(actionContext.ModelState)));
                return;
            }
            await next();
        }
    }
}
