using Route02.BLL.DTO;
using Route02.BLL.DTO.Department;

namespace Route02.BLL.Factories.Department;

/// <summary>
///     That Class To Transfer Data From Department Entity To Map Data (Manul Data)
/// </summary>
public static class DepartmentFactory
{
    
    // Get all departments
    public static GetAllDepartmentsDto ToGetAllDepartmentsDto(this DAL.Models.Department.Department department)
    {
        return new GetAllDepartmentsDto()
        {
            DeptId = department.Id,
            Name = department.Name,
            Code = department.Code,
            Description = department.Description,
            CreatedOn = department.CreatedOn
        };
    }

    // Get department by ID
    public static GetDepartmentByIdDto ToGetDepartmentByIdDto(this DAL.Models.Department.Department department)
    {
        return new GetDepartmentByIdDto()
        {
            Id = department.Id,
            Name = department.Name,
            Code = department.Code,
            Description = department.Description,
            CreatedBy = department.CreatedBy,
            CreatedOn = department.CreatedOn,
            LastModificationBy = department.LastModificationBy,
            LastModificationOn = department.LastModificationOn,
            IsDeleted = department.IsDeleted
        };
    }
    
    // Add a new department
    public static DAL.Models.Department.Department ToAddDepartmentDto(this CreateDepartmentDto departmentDtO)
    {
        return new DAL.Models.Department.Department()
        {
            Name = departmentDtO.Name,
            Code = departmentDtO.Code,
            Description = departmentDtO.Description,
            CreatedOn = departmentDtO.CreatedOn
        };
    }
    
    // Update an existing department
    public static DAL.Models.Department.Department ToUpdateDepartmentDto(this UpdateDepartmentDto departmentDtO)
    {
        return new DAL.Models.Department.Department()
        {
            Id = departmentDtO.Id,
            Name = departmentDtO.Name,
            Code = departmentDtO.Code,
            Description = departmentDtO.Description,
            CreatedOn = departmentDtO.CreatedOn
        };
    }
}