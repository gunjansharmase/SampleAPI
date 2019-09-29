using AutoMapper;
using SampleAPI.DataTransferObjects.DTO.Packaging;
using SampleAPI.ORM.Model.Packaging;
using log4net;
using Microsoft.Extensions.Options;
using SampleAPI.DataTransferObjects;
using SampleAPI.Interface.DataAccess;
using SampleAPI.Interface.DataAccess.Packaging;
using SampleAPI.ORM;
using SampleAPI.Service.Interface.Packaging;

namespace SampleAPI.Service.Services.Packaging
{
    public class CustomerService : Service<CustomerDTO, Customer>, ICustomerService
    {
        protected readonly ICustomerRepository<Customer> _ddRepository;
        protected readonly ConfigurationSettings _configurationSettings;
        protected readonly IMapper _mapper;
        protected readonly ILog _logger;
        public CustomerService(IUnitOfWork unitOfWork, ILog logger, IDbHelper idbHelper, ICustomerRepository<Customer> ddRepository, IMapper mapper, IOptions<ConfigurationSettings> configurationSettings) : base(unitOfWork, idbHelper, mapper)
        {
            _ddRepository = ddRepository;
            _mapper = mapper;
            _configurationSettings = configurationSettings.Value;
            _logger = logger;
        }


    }
}
