using AutoMapper;
using CloudSaaSCodingTask.Core.Dtos;
using CloudSaaSCodingTask.Core.Entities;

namespace CloudSaaSCodingTask.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, ViewEmployeeDto>().ReverseMap();
            CreateMap<Employee, AddEmployeeDto>().ReverseMap();
            CreateMap<Employee, EditEmployeeDto>().ReverseMap();
        }
    }
}
