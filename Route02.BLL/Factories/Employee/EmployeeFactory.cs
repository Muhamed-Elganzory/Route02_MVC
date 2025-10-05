using Route02.BLL.DTO.Employee;

namespace Route02.BLL.Factories.Employee;

/// <summary>
///     Don't Use It, We Use Auto Mapper
/// </summary>
public static class EmployeeFactory
{
    // Get all employees
    public static GetAllEmployeeDto ToGetAllEmployeeDto(this DAL.Models.Employee.Employee employee)
    {
        return new GetAllEmployeeDto()
        {
            Id = employee.Id,
            Name = employee.Name,
            Age = employee.Age,
            Address = employee.Address,
            IsActive = employee.IsActive,
            Salary = employee.Salary,
            Email = employee.Email,
            Gender = employee.Gender,
            EmployeeType = employee.EmployeeType
        };
    }
    
    // Get employee by ID
    public static GetEmployeeDetailsDto ToGetEmployeeDetailsDto (this DAL.Models.Employee.Employee employee)
    {
        return new GetEmployeeDetailsDto()
        {
            Id = employee.Id,
            Name = employee.Name,
            Age = employee.Age,
            Address = employee.Address,
            IsActive = employee.IsActive,
            Salary = employee.Salary,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            HiringDate = employee.HiringDate,
            EmpGender = employee.Gender,
            EmployeeType = employee.EmployeeType
        };
    }
    
    // Add a new employee
    public static DAL.Models.Employee.Employee ToAddEmployeeDto(this CreateEmployeeDto employee)
    {
        return new DAL.Models.Employee.Employee()
        {
            Name = employee.Name,
            Age = employee.Age,
            Address = employee.Address,
            IsActive = employee.IsActive,
            Salary = employee.Salary,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            HiringDate = employee.HiringDate,
            Gender = employee.Gender,
            EmployeeType = employee.EmployeeType,
            CreatedBy = employee.CreatedBy,
            LastModificationBy = employee.LastModificationBy
        };
    }
    
    // Update an existing employee
    public static DAL.Models.Employee.Employee ToUpdateEmployeeDto(this UpdateEmployeeDto employee)
    {
        return new DAL.Models.Employee.Employee()
        {
            Name = employee.Name,
            Age = employee.Age,
            Address = employee.Address,
            IsActive = employee.IsActive,
            Salary = employee.Salary,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            HiringDate = employee.HiringDate,
            Gender = employee.Gender,
            EmployeeType = employee.EmployeeType,
            CreatedBy = employee.CreatedBy,
            LastModificationBy = employee.LastModificationBy
        };
    }
}