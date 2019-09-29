using AutoMapper;
using SampleAPI.DataTransferObjects.DTO.Packaging;
using SampleAPI.ORM.Model.Packaging;

namespace SampleAPI.Service.AutomapperProfiles
{
    public class MainAutomapperProfile: Profile
    {
        public MainAutomapperProfile()
        {
            //For Packaging Application
            CreateMap<CustomerDTO, Customer>().ReverseMap();
            
        }
    }
}