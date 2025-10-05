using AutoMapper;
using Route02.BLL.DTO.Department;


namespace Route02.BLL.Mapping.Department;

public class DepartmentMappingProfile: Profile
{
    public DepartmentMappingProfile()
    {
        // Get All Department
        CreateMap<DAL.Models.Department.Department, GetAllDepartmentsDto>();
        
        // Get Department By ID
        CreateMap<DAL.Models.Department.Department, GetDepartmentByIdDto>();
        
        // Create - Add Department
        CreateMap<CreateDepartmentDto, DAL.Models.Department.Department>();
        
        // Update Department
        CreateMap<UpdateDepartmentDto, DAL.Models.Department.Department>();
    }
}