using log4net;
using SampleAPI.DataTransferObjects.DTO.Packaging;
using SampleAPI.ORM.Model.Packaging;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Service.Interface.Packaging;


namespace SampleAPI.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class CustomerController : BaseController<CustomerDTO, Customer, CustomerDTO>
    {
        protected readonly ICustomerService _CustomerService;
        public CustomerController(ICustomerService service) : base(service)
        {
            _CustomerService = service;
        }
    }
}
