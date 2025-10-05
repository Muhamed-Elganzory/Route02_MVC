using AutoMapper;
using Route02.BLL.DTO.Employee;

namespace Route02.BLL.Mapping.Employee;

public class EmployeeMappingProfile : Profile
{
    public EmployeeMappingProfile()
    {
        // Get All Employee
        CreateMap<DAL.Models.Employee.Employee, GetAllEmployeeDto>()
            .ForMember(dest => dest.DepartmentName, opt =>
                opt.MapFrom(src => src.EmployeeWorkDepartment != null ? src.EmployeeWorkDepartment.Name : null))
            .ForMember(dest => dest.DepartmentId, opt =>
                opt.MapFrom(src => src.FkDepartmentId));

        // Get Employee Details
        CreateMap<DAL.Models.Employee.Employee, GetEmployeeDetailsDto>()
            // Gender
            .ForMember(dest => dest.EmpGender, opt => opt.MapFrom(src => src.Gender))

            // Department
            .ForMember(dest => dest.DepartmentName, opt =>
                opt.MapFrom(src => src.EmployeeWorkDepartment != null ? src.EmployeeWorkDepartment.Name : null))
            .ForMember(dest => dest.DepartmentId, opt =>
                opt.MapFrom(src => src.FkDepartmentId))

            // Map Image Property To Check If The Image Name Is The Same Image Name
            .ForMember(dest => dest.ImageName, opt =>
                opt.MapFrom(src => src.ImageName != null ? src.ImageName : null));

        // Add - Create Employee
        CreateMap<CreateEmployeeDto, DAL.Models.Employee.Employee>();

        // Update Employee → Ignore CreatedOn
        CreateMap<UpdateEmployeeDto, DAL.Models.Employee.Employee>()
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore());
    }
}
