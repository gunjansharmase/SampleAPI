using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SampleAPI.Interface.Services
{
    public interface IValidationService
    {
        Task<bool> ValidateAsync(IDictionary<string, object> actionArguments, ActionDescriptor actionDescriptor, ModelStateDictionary modelStateDictionary);
    }
}
