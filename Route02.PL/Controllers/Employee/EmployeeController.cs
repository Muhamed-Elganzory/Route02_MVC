using Microsoft.AspNetCore.Mvc;
using Route02.BLL.DTO.Employee;
using Route02.BLL.Services.Employee;
using Route02.DAL.Models.Employee.Enum;
using Route02.PL.Controllers.Department;
using Route02.PL.ViewModels.EmployeeViewModel;

namespace Route02.PL.Controllers.Employee;

public class EmployeeController(IEmployeeService employeeService, ILogger<DepartmentController> logger, IWebHostEnvironment webHostEnvironment): Controller
{
    // Get All Employee
    [HttpGet]
    public IActionResult Index()
    {
        var employees = employeeService.GetAllEmployees(false);
        return View(employees);
    }
    
    // Get Employee Details
    [HttpGet]
    public IActionResult Details(int? id)
    {
        if (id == null) return NotFound();
        var employee = employeeService.GetEmployeeById(id);
        
        return View(employee);
    }
    
    // Create - Add Employee
    [HttpGet]
    public IActionResult Create()
    {
        return View(new AddEmployeeDto());
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create (AddEmployeeDto employeeDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // If Is Successful Redirect To Master Page To Print The New Data
                int result = employeeService.AddEmployee(employeeDto);
                
                if (result > 0) return RedirectToAction(nameof(Index));
                
                // Else Return Message and Return To Create Page (Form To Try Again)
                ModelState.AddModelError(string.Empty, "Employee Can Not Be Created...!");
            }
            catch (Exception e)
            {
                // 1- Development
                // Print Error In Kestrel Console
                if (webHostEnvironment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    // return View(addDepartmentDto);
                }
                // 2- Deployment
                else
                {
                    logger.LogError(e.Message);
                    // return View(addDepartmentDto);
                }
            }
        }
        else
        {
            return View(employeeDto);
        }
        
        // Return The Same Data, It Is Prefer, Instead of Return To Empty Form
        return RedirectToAction(nameof(Index));
    }

    // Update - Edite
    [HttpGet]
    public IActionResult Edit([FromRoute]int? id)
    {
        if(!id.HasValue) return BadRequest();
        
        // Get Employee
        var employee = employeeService.GetEmployeeById(id);
        if(employee is null) return NotFound();
        
        // Mapping
        var updatedEmployee = new EmployeeViewModels()
        {
            Id = employee.Id,
            Name = employee.Name,
            Address = employee.Address,
            Age = employee.Age,
            Salary = employee.Salary,
            EmployeeType = Enum.TryParse<EmployeeType>(employee.EmployeeType, out var empType) ? empType : default,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            HiringDate = employee.HiringDate,
            Gender = Enum.TryParse<Gender>(employee.EmpGender, out var gender) ? gender : default,
            IsActive = employee.IsActive,
        };
        
        return View(updatedEmployee);
    }
    
    // Update - Edite
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit([FromRoute]int? id, EmployeeViewModels employeeViewModels)
    {
        if(!id.HasValue) return BadRequest();
        if (!ModelState.IsValid) return View(employeeViewModels);
    
        try
        {
            var updatedEmployee = new UpdateEmployeeDto()
            {
                Id = id.Value,
                Name = employeeViewModels.Name,
                Address = employeeViewModels.Address,
                Age = employeeViewModels.Age,
                Salary = employeeViewModels.Salary,
                EmployeeType = employeeViewModels.EmployeeType,
                Email = employeeViewModels.Email,
                PhoneNumber = employeeViewModels.PhoneNumber,
                HiringDate = employeeViewModels.HiringDate,
                Gender = employeeViewModels.Gender,
                IsActive = employeeViewModels.IsActive,
            };
        
            int result = employeeService.UpdateEmployee(updatedEmployee);

            if (result > 0) return RedirectToAction(nameof(Index));
        
            ModelState.AddModelError(string.Empty, "Employee Can Not Be Updated...!");
            return View(employeeViewModels);
        }
        catch (Exception e)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                ModelState.AddModelError(string.Empty, "Employee Can Not Be Updated...!");
                return View(employeeViewModels);
            }
            else
            {
                logger.LogError(e.Message);
                return View(employeeViewModels);
            }
        }
    }
    
    // Delete
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete([FromRoute]int? id)
    {
        if (id == null || id == 0) return BadRequest();

        try
        {
            bool result = employeeService.DeleteEmployee(id);

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty,  "Department Can Not Delete...!");
                return RedirectToAction(nameof(Delete), new { id = id });
            }
        }
        catch (Exception e)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                ModelState.AddModelError(string.Empty,  "Department Can Not Delete...!");
            }
            else
            {
                logger.LogError(e.Message);
            }
        }
        
        return RedirectToAction(nameof(Index));
    }
}