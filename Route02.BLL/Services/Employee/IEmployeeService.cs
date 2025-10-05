using Route02.BLL.DTO.Employee;

namespace Route02.BLL.Services.Employee;

public interface IEmployeeService
{
    // Get all employees
    IEnumerable <GetAllEmployeeDto> GetAllEmployees(bool withTracking);
    
    // Get employee by ID
    GetEmployeeDetailsDto? GetEmployeeById (int id);
    
    // Add a new employee
    int CreateEmployee(CreateEmployeeDto employeeDto);
    
    // Update an existing employee
    int UpdateEmployee(UpdateEmployeeDto employeeDto);
    
    // Delete an employee by ID
    bool DeleteEmployee(int? id);
}