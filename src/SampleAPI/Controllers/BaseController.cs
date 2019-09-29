using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SampleAPI.Filters;
using SampleAPI.Interface.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleAPI.Api.Controllers
{
    [ServiceFilter(typeof(ValidateActionFilterAttribute))]
    [ServiceFilter(typeof(ExceptionActionFilterAttribute))]
    public abstract class BaseController<T,U,V>: Controller
        where U : class
        where T : class
        where V : class
    {
        protected readonly IService<T,U> _service;
        public BaseController(IService<T, U> service)
        {
            _service = service;
        }

        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync([FromBody]T request)
        {
            var result = await _service.AddAsync(request).ConfigureAwait(false);
            return result.Success ? (IActionResult)Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public virtual async Task<IActionResult> UpdateAsync([FromBody]V request)
        {
            var result = await _service.UpdateAsync(request as T).ConfigureAwait(false);
             if (result.Message.Contains("_ERROR_NON_EXISTENT"))
                return NotFound(result.ResponseObject);

            return result.Success ? (IActionResult)Ok(result) : BadRequest(result);
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAsync());
        }

        [HttpGet("Id")]
        public virtual async Task<IActionResult> GetByIdAsync([FromQuery]int requestId)
        {
            if (requestId <= 0)
                return new BadRequestObjectResult("ID must be greater than zero");

            var result = await _service.GetByIdAsync(requestId);
            //return Ok(await _service.GetByIdAsync(requestId));
            return result.Success ? (IActionResult)Ok(result) : NotFound(requestId);
        }

        [HttpDelete]
        public virtual async Task<IActionResult> DeleteByIdAsync([FromQuery]int requestId)
        {
            if (requestId <= 0)
                return new BadRequestObjectResult("ID must be greater than zero");

            var result = await _service.DeleteAsync(requestId);

            return result.Success ? (IActionResult)Ok(result) : NotFound(requestId);

        }
    }
}
