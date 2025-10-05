using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Route02.BLL.DTO.Employee;
using Route02.BLL.Services.Department;
using Route02.BLL.Services.Employee;
using Route02.DAL.Models.Employee.Enum;
using Route02.PL.Controllers.Department;
using Route02.PL.ViewModels.EmployeeViewModel;

namespace Route02.PL.Controllers.Employee;

public class EmployeeController(IEmployeeService employeeService, ILogger<DepartmentController> logger, IWebHostEnvironment webHostEnvironment, IDepartmentService departmentService): Controller
{
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly IWebHostEnvironment _environment = webHostEnvironment;
    private readonly ILogger _logger = logger;
    private readonly IDepartmentService _departmentService = departmentService;

    // Get All Employee
    [HttpGet]
    public IActionResult Index(string? employeeSearchName)
    {
        var employees = _employeeService.GetAllEmployees(false);

        if (!string.IsNullOrWhiteSpace(employeeSearchName))
        {
            employees = employees.Where(e => e.Name.Contains(employeeSearchName, StringComparison.OrdinalIgnoreCase));
        }

        return View(employees);
    }
    
    // Get Employee Details
    [HttpGet]
    public IActionResult Details(int? id)
    {
        if (!id.HasValue) return BadRequest(); // 400

        var employee = _employeeService.GetEmployeeById(id.Value);

        if (employee is null) return NotFound(); // 404
        // var model = departmentService.GetDepartmentById(employee.DepartmentId);
        // employee.DepartmentName = model.Name;
        // model.Id == employee.DepartmentId
        
        return View(employee);
    }
    
    // Create - Add Employee
    [HttpGet]
    public IActionResult Create()
    {
        var model = new EmployeeViewModels()
        {
            Departments = _departmentService.GetAllDepartments()
                .Select(dept => new SelectListItem
                {
                    Value = dept.DeptId.ToString(),
                    Text = dept.Name
                })
        };

        foreach (var d in model.Departments)
        {
            Console.WriteLine($"{d.Value} - {d.Text}");
        }

        return View(model);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EmployeeViewModels employeeViewModels)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var employeeDto = new CreateEmployeeDto()
                {
                    Name = employeeViewModels.Name,
                    Age = employeeViewModels.Age,
                    Email = employeeViewModels.Email,
                    Address = employeeViewModels.Address,
                    PhoneNumber = employeeViewModels.PhoneNumber,
                    Salary = employeeViewModels.Salary,
                    EmployeeType = employeeViewModels.EmployeeType,
                    Gender = employeeViewModels.Gender,
                    HiringDate = employeeViewModels.HiringDate,
                    IsActive = employeeViewModels.IsActive,
                    DepartmentId = employeeViewModels.DepartmentId,
                    Image = employeeViewModels.Image
                };

                int result = _employeeService.CreateEmployee(employeeDto);

                if (result > 0)
                {
                    string message = $"Department: { employeeViewModels.Name } Created Successfully";

                    TempData["message"] = message;

                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Employee Can Not Be Created...!");
            }
            catch (Exception e)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
                else
                {
                    _logger.LogError(e.Message);
                    ModelState.AddModelError(string.Empty, "Unexpected error occurred. Please try again later.");
                }
            }
        }
        else
        {
            var error = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            ViewBag.Error = error;
        }

        employeeViewModels.Departments = _departmentService.GetAllDepartments()
            .Select(dept => new SelectListItem
            {
                Value = dept.DeptId.ToString(),
                Text = dept.Name,
                Selected = dept.DeptId == employeeViewModels.DepartmentId
            });

        return View(employeeViewModels);
    }

    // Update - Edite
    [HttpGet]
    public IActionResult Edit([FromRoute]int? id)
    {
        if(!id.HasValue) return BadRequest();
        
        // Get Employee
        var employee = _employeeService.GetEmployeeById(id.Value);

        if(employee is null) return NotFound();

        // Mapping
        var updatedEmployee = new EmployeeViewModels()
        {
            Id = employee.Id,
            Name = employee.Name,
            Address = employee.Address,
            Age = employee.Age,
            Salary = employee.Salary,
            EmployeeType = employee.EmployeeType, // Enum.TryParse<EmployeeType>(employee.EmployeeType, out var empType) ? empType : default,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            HiringDate = employee.HiringDate,
            Gender = employee.EmpGender, // Enum.TryParse<Gender>(employee.EmpGender, out var gender) ? gender : default,
            IsActive = employee.IsActive,
            DepartmentId = employee.DepartmentId,
        };

        updatedEmployee.Departments = _departmentService.GetAllDepartments()
            .Select(department => new SelectListItem()
            {
                Value = department.DeptId.ToString(),
                Text = department.Name,
                Selected = department.DeptId == employee.DepartmentId // Foreign Key
            });

        return View(updatedEmployee);
    }

    // Update - Edite
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit([FromRoute]int? id, EmployeeViewModels employeeViewModels)
    {
        if(!id.HasValue) return BadRequest();

        if (!ModelState.IsValid)
        {
            employeeViewModels.Departments = _departmentService.GetAllDepartments()
                .Select(department => new SelectListItem()
                {
                    Value = department.DeptId.ToString(),
                    Text = department.Name,
                    Selected = department.DeptId == employeeViewModels.DepartmentId
                });

            return View(employeeViewModels);
        }

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
                DepartmentId = employeeViewModels.DepartmentId,
            };

            int result = _employeeService.UpdateEmployee(updatedEmployee);

            if (result > 0) return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Employee Can Not Be Updated...!");

            employeeViewModels.Departments = _departmentService.GetAllDepartments()
                .Select(department => new SelectListItem()
                {
                    Value = department.DeptId.ToString(),
                    Text = department.Name,
                    Selected = department.DeptId == employeeViewModels.DepartmentId
                });

            return View(employeeViewModels);
        }
        /*catch (Exception e)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                ModelState.AddModelError(string.Empty, e.Message);

                employeeViewModels.Departments = departmentService.GetAllDepartments()
                    .Select(department => new SelectListItem()
                    {
                        Value = department.DeptId.ToString(),
                        Text = department.Name,
                        Selected = department.DeptId == employeeViewModels.DepartmentId
                    });
            }

            logger.LogError(e.Message);
            // return View(employeeViewModels);

            return View("Error");
        }*/
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating employee");
            ModelState.AddModelError(string.Empty, e.Message);

            employeeViewModels.Departments = _departmentService.GetAllDepartments()
                .Select(dept => new SelectListItem
                {
                    Value = dept.DeptId.ToString(),
                    Text = dept.Name,
                    Selected = dept.DeptId == employeeViewModels.DepartmentId
                });

            return View(employeeViewModels);
        }
    }
    
    // Delete
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        if (id <= 0) return BadRequest();

        try
        {
            bool result = _employeeService.DeleteEmployee(id);

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
            if (_environment.IsDevelopment())
            {
                ModelState.AddModelError(string.Empty,  "Department Can Not Delete...!");
            }
            else
            {
                _logger.LogError(e.Message);
            }
        }
        
        return RedirectToAction(nameof(Index));
    }
}