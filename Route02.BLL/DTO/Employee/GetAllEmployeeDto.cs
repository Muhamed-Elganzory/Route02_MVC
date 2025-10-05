using System.ComponentModel.DataAnnotations;
using Route02.DAL.Models.Employee.Enum;

namespace Route02.BLL.DTO.Employee;

public class GetAllEmployeeDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public int Age { get; set; }
    
    public string? Address { get; set; }
    
    [Display (Name = "Is Active")]
    public bool IsActive { get; set; }
    
    [DataType(DataType.Currency)]
    public decimal Salary { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; } = null!;
    
    // [Display (Name = "Employee Gender")]
    [Display (Name = "Gender")]
    public Gender Gender { get; set; }

    [Display (Name = "Employee Type")]
    public EmployeeType EmployeeType { get; set; }

    [Display (Name = "Department")]
    public int? DepartmentId { get; set; } // Department ID Represent Foreign Key

    public string? DepartmentName { get; set; } // Department Name
}