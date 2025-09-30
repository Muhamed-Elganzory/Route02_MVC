using AutoMapper;
using Route02.BLL.DTO.Employee;
// using Route02.BLL.Factories.Employee;
using Route02.DAL.Repositories.Employee;

namespace Route02.BLL.Services.Employee;

public class EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper): IEmployeeService
{
    // Get all employees
    public IEnumerable<GetAllEmployeeDto> GetAllEmployees(bool withTracking = false)
    {
        var employees = employeeRepository.GetAll (withTracking);
        
        // Source => Employee
        // Destination => GetAllEmployeeDto
        
        // Using Auto Mapper
        var employeeDto = mapper.Map<IEnumerable<DAL.Models.Employee.Employee>, IEnumerable<GetAllEmployeeDto>>(employees);
        return employeeDto;
        
        // Using Factory Mapping
        // return employee.Select(empFactory => empFactory.ToGetAllEmployeeDto());
    }

    // Get employee by ID
    public GetEmployeeDetailsDto? GetEmployeeById (int? id)
    {
        var employee = employeeRepository.GetById (id);
        
        if (employee == null) return null;
        
        // Using Auto Mapper
        var employeeDto = mapper.Map<DAL.Models.Employee.Employee, GetEmployeeDetailsDto>(employee);
        return employeeDto;
        
        // Using Factory Mapping
        // return employee is null ? null : employee.ToGetEmployeeDetailsDto();
    }

    // Add a new employee
    public int AddEmployee(AddEmployeeDto employee)
    {
        // Using Factory Mapping
        // var employeeDto = employee.ToAddEmployeeDto();
        // return employeeRepository.Add (employeeDto);

        // Using Auto Mapper
        var employeeDto = mapper.Map<AddEmployeeDto, DAL.Models.Employee.Employee>(employee);
        return employeeRepository.Add(employeeDto);
    }

    // Update employee
    public int UpdateEmployee(UpdateEmployeeDto employee)
    {
        // Using Factory Mapping
        // var employee = employeeDto.ToUpdateEmployeeDto();
        // return employeeRepository.Update (employee);

        // Using Auto Mapper
        var employeeDto = mapper.Map<UpdateEmployeeDto, DAL.Models.Employee.Employee>(employee);
        return employeeRepository.Update(employeeDto);
    }
    
    // Delete employee
    public bool DeleteEmployee(int? id)
    {
        var employee = employeeRepository.GetById (id);
        
        if (employee is not null)
        {
            // Soft Deleted
            employee.IsDeleted = true;
            employee.IsActive = false;
            return employeeRepository.Update(employee) > 0 ? true : false;
            
            // Delete Using Remove Function
            // var result = employeeRepository.Delete (employee);
            // return result > 0 ? true : false;
        }
        
        return false;
    }
}