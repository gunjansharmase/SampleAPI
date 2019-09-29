using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using log4net;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SampleAPI.Service;
using SampleAPI.Interface.Services;

namespace SampleAPI.Service
{
    public class ValidationService : IValidationService
    {
        protected readonly ILog _logger;
        protected readonly IValidatorFactory _serviceProviderValidatorFactory;

        public ValidationService(ILog loggerFactory, IValidatorFactory serviceProviderValidatorFactory)
        {
            _logger = loggerFactory;
            _serviceProviderValidatorFactory = serviceProviderValidatorFactory;
        }
         
        public async Task<bool> ValidateAsync(IDictionary<string, object> actionArguments, ActionDescriptor actionDescriptor, ModelStateDictionary modelStateDictionary)
        {
            if (actionArguments == null || actionArguments.Count == 0)
                return modelStateDictionary.IsValid;

            //Ignore value type arguments. Validaton framework handles only Reference types
            var referenceTypeParameters = actionDescriptor.Parameters.Where(c => !c.ParameterType.GetTypeInfo().IsValueType).Select(c => c.Name);

            if (!referenceTypeParameters.Any()) { return modelStateDictionary.IsValid; }

            var referenceActionArguments = actionArguments.Where(c => referenceTypeParameters.Contains(c.Key));

            foreach (var argument in referenceActionArguments)
            {
                //If it is null...raise IServiceresponse Null reference exception
                if (argument.Value == null)
                {
                    ValidationFailure validationFailure = new ValidationFailure(argument.Key, ServiceResponseMessage.GenericAddErrorNull);
                    (new ValidationResult(new List<ValidationFailure> { validationFailure })).AddToModelState(modelStateDictionary, string.Empty);
                    break;
                }

                //Check validator exists of that argument
                var myCustomDTOValidator = _serviceProviderValidatorFactory.GetValidator(argument.Value.GetType());

                //If validator is null then DTO is not decorated Or validator not available.
                if (myCustomDTOValidator == null) { continue; }

                var result = await myCustomDTOValidator.ValidateAsync(argument.Value);                    
                result.AddToModelState(modelStateDictionary, string.Empty);
            }

            return modelStateDictionary.IsValid;
        }
    }
}
