using SampleAPI.DataTransferObjects.DTO.Packaging;
using SampleAPI.ORM.Model.Packaging;
using SampleAPI.DataTransferObjects.Interface;
using SampleAPI.Interface.Services;

namespace SampleAPI.Service.Interface.Packaging
{
    public interface ICustomerService : IService<CustomerDTO, Customer>
    {
    }
}
