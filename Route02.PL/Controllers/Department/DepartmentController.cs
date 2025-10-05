using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route02.BLL.DTO.Department;
using Route02.BLL.Services.Department;
using Route02.PL.ViewModels.DepartmentViewModels;

namespace Route02.PL.Controllers.Department;

public class DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment webHostEnvironment): Controller
{
    private readonly IDepartmentService _departmentService = departmentService;
    private readonly ILogger<DepartmentController> _logger = logger;
    private readonly IWebHostEnvironment _environment = webHostEnvironment;

    [HttpGet]
    public IActionResult Index(string? departmentSearchName)
    {
        /*ViewData["Message1"] = "ViewData";
        ViewBag.Message2 = "ViewBag";
        ViewBag.Message = new GetAllDepartmentsDto() { Name = "View Bag" };
        ViewData["Message"] = new GetAllDepartmentsDto() { Name = "View Data" };*/
        
        var departments = _departmentService.GetAllDepartments();

        if (departmentSearchName != null)
        {
            departments = departments.Where(department => department.Name.Contains(departmentSearchName, StringComparison.OrdinalIgnoreCase));
        }

        return View( departments);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateDepartmentDto createDepartmentDto)
    {
        // Server Side Validation
        if (ModelState.IsValid)
        {
            try
            {
                int result = _departmentService.AddDepartment(createDepartmentDto);
                string message;
                
                // If Is Successful Redirect To Master Page To Print The New Data
                if (result > 0)
                {
                    message = $"Department: { createDepartmentDto.Name } Created Successfully";
                    
                    TempData["message"] = message;

                    return RedirectToAction(nameof(Index));
                }
                
                // Else Return Message and Return To Create Page (Form To Try Again)
                ModelState.AddModelError(string.Empty, "Department Can Not Be Created...!");
                
                // Return The Same Data, It Is Prefer, Instead of Return To Empty Form
                // return View(createDepartmentDto);
                
                message = $"Department: { createDepartmentDto.Name } Not Created";
                TempData["message"] = message;
                
                // return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                // 1- Development
                // Print Error In Kestrel Console
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    // return View(createDepartmentDto);
                }
                // 2- Deployment
                else
                {
                    _logger.LogError(e.Message);
                    // return View(createDepartmentDto);
                }
            }
        }
        
        // Return The Same Data, It Is Prefer, Instead of Return To Empty Form
        return View(createDepartmentDto);
    }

    [HttpGet]
    public IActionResult Details(int? id)
    {
        if (!id.HasValue) return BadRequest(); // 400

        var department = _departmentService.GetDepartmentById(id.Value);
        
        if (department is null) return NotFound();
        
        return View(department);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (!id.HasValue) return BadRequest(); // 400

        var department = _departmentService.GetDepartmentById(id.Value);

        if (department is null) return NotFound();

        var departmentViewModel = new DepartmentViewModel()
        {
            Code = department.Code,
            Name = department.Name,
            CreatedOn = department.CreatedOn,
            Description = department.Description
        };

        return View(departmentViewModel);
       // return View(new DepartmentViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit([FromRoute] int? id, DepartmentViewModel departmentViewModel)
    {
        if (!id.HasValue) return BadRequest(); // 400

        if (!ModelState.IsValid) return View(departmentViewModel);

        try
        {
            var updateDepartment = new UpdateDepartmentDto()
            {
                Id = id.Value,
                Name = departmentViewModel.Name,
                Code = departmentViewModel.Code,
                Description = departmentViewModel.Description,
                CreatedOn = departmentViewModel.CreatedOn
            };

            var result = _departmentService.UpdateDepartment(updateDepartment);

            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Department Can Not Be Updated...!");
        }
        catch (Exception e)
        {
            // 1- Development
            // Print Error In Kestrel Console
            if (_environment.IsDevelopment())
            {
                ModelState.AddModelError(string.Empty, "Department Can Not Be Created...!");
            }

            // 2- Deployment
            else
            {
                _logger.LogError(e.Message);
                return View("Error");
            }
        }

        return View(departmentViewModel);
    }
    
    /*[HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id is null) return BadRequest();
        
        var department = departmentService.GetDepartmentById(id);

        if (department is null) return NotFound();

        // departmentService.DeleteDepartment(id.Value);
        
        return View(department);
    }*/

    // Delete
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete([FromRoute] int id)
    {
        if(id <= 0) return BadRequest(); // 400
        
        try
        {
            bool isDeleted = _departmentService.DeleteDepartment(id);
            
            if (isDeleted)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty,  "Department Can Not Delete...!");
                return RedirectToAction(nameof(Delete), new { id });
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