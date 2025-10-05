using AutoMapper;
using Route02.BLL.DTO.Employee;
using Route02.BLL.Services.AttachmentService.Interface;
// using Route02.BLL.Factories.Employee;
using Route02.DAL.Repositories.Employee;
using Route02.DAL.Repositories.Interfaces;

namespace Route02.BLL.Services.Employee;

// IEmployeeRepository employeeRepository ==> IUnitOfWork unitOfWork
public class EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, IAttachmentService attachmentService): IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IAttachmentService _attachmentService = attachmentService;

    // Get all employees
    public IEnumerable<GetAllEmployeeDto> GetAllEmployees(bool withTracking = false)
    {
        var employees = _unitOfWork.EmployeeRepository.GetAll (withTracking);
        
        // Source => Employee
        // Destination => GetAllEmployeeDto
        
        // Using Auto Mapper
        var employeeDto = _mapper.Map<IEnumerable<DAL.Models.Employee.Employee>, IEnumerable<GetAllEmployeeDto>>(employees);
        return employeeDto;
        
        // Using Factory Mapping
        // return employee.Select(empFactory => empFactory.ToGetAllEmployeeDto());
    }

    // Get employee by ID
    public GetEmployeeDetailsDto? GetEmployeeById (int id)
    {
        var employee = _unitOfWork.EmployeeRepository.GetById (id);
        
        if (employee == null) return null;
        
        // Using Auto Mapper
        var employeeDto = _mapper.Map<DAL.Models.Employee.Employee, GetEmployeeDetailsDto>(employee);
        return employeeDto;
        
        // Using Factory Mapping
        // return employee is null ? null : employee.ToGetEmployeeDetailsDto();
    }

    // Add a new employee
    public int CreateEmployee(CreateEmployeeDto employeeDto)
    {
        // Using Factory Mapping
        // var employeeDto = employee.ToAddEmployeeDto();
        // return employeeRepository.Add (employeeDto);

        // Using Auto Mapper
        var employee = _mapper.Map<CreateEmployeeDto, DAL.Models.Employee.Employee>(employeeDto);

        if (employeeDto.Image is not null)
        {
            employee.ImageName = _attachmentService.Upload(employeeDto.Image, "Images");
        }

        _unitOfWork.EmployeeRepository.Add(employee);

        return _unitOfWork.SaveChanges();
    }

    // Update employee
    public int UpdateEmployee(UpdateEmployeeDto employee)
    {
        // Using Factory Mapping
        // var employee = employeeDto.ToUpdateEmployeeDto();
        // return employeeRepository.Update (employee);

        // Using Auto Mapper
        var employeeDto = _mapper.Map<UpdateEmployeeDto, DAL.Models.Employee.Employee>(employee);
        _unitOfWork.EmployeeRepository.Update(employeeDto);

        return _unitOfWork.SaveChanges();
    }
    
    // Delete employee
    public bool DeleteEmployee(int? id)
    {
        var employee = _unitOfWork.EmployeeRepository.GetById (id);
        
        if (employee is not null)
        {
            // Soft Deleted
            employee.IsDeleted = true;
            employee.IsActive = false;
            _unitOfWork.EmployeeRepository.Update(employee);

            return _unitOfWork.SaveChanges() > 0 ? true : false;
            
            // Delete Using Remove Function
            // var result = employeeRepository.Delete (employee);
            // return result > 0 ? true : false;
        }
        
        return false;
    }
}