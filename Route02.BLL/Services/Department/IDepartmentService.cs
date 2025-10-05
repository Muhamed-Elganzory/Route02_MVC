using Route02.BLL.DTO;
using Route02.BLL.DTO.Department;

namespace Route02.BLL.Services.Department;

public interface IDepartmentService
{
    // Get all departments
    IEnumerable<GetAllDepartmentsDto> GetAllDepartments();

    // Get department by ID
    GetDepartmentByIdDto? GetDepartmentById(int id);

    // Add a new department
    int AddDepartment(CreateDepartmentDto department);

    // Update an existing department
    int UpdateDepartment(UpdateDepartmentDto departmentDto);

    // Delete a department by ID
    bool DeleteDepartment(int id);
}