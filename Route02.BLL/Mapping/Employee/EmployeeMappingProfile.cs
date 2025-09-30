using AutoMapper;
using Route02.BLL.DTO.Employee;

namespace Route02.BLL.Mapping.Employee;

public class EmployeeMappingProfile: Profile
{
    public EmployeeMappingProfile()
    {
        // Get All Employee
        CreateMap<DAL.Models.Employee.Employee, GetAllEmployeeDto>();
        
        // Get Employee Details
        CreateMap<DAL.Models.Employee.Employee, GetEmployeeDetailsDto>()
            .ForMember(destination => destination.EmpGender, option => option.MapFrom(src => src.Gender));
        
        // Add - Create Employee
        CreateMap<AddEmployeeDto, DAL.Models.Employee.Employee>();
        
        // Update Employee
        CreateMap<UpdateEmployeeDto, DAL.Models.Employee.Employee>();
    }
}